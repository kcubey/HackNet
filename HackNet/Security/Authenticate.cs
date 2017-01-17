using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

using HackNet.Data;
using HackNet.Loggers;
using HackNet.Game.Class;

namespace HackNet.Security
{
	internal class Authenticate : IDisposable
	{
		internal string Email { get; private set; }

		internal KeyStore TempKeyStore { get; private set; }

		Stopwatch sw = new Stopwatch();

		internal Authenticate()
		{
			Email = GetCurrentUser(ReadOnly: true).Email;
			Debug.WriteLine("Creating new authenticate instance for " + Email);
		}

		internal Authenticate(string email)
		{
			// To ensure email casing is correct
			Email = Users.FindByEmail(email: email).Email;
			Debug.WriteLine("Creating new authenticate instance for " + Email);
		}

		/*
		 * PASSWORD CONF INSTANCE METHODS
		 */
		internal AuthResult ValidateLogin(string password, bool checkEmailValidity = true)
		{
			using (DataContext db = new DataContext())
			{
				Users user = Users.FindByEmail(this.Email, db);
				if (user == null)
				{
					AuthLogger.Instance.UserNotFound(Email);
					return AuthResult.UserNotFound;
				}
				if (checkEmailValidity && user.AccessLevel == AccessLevel.Unverified)
					return AuthResult.EmailNotVerified;

				byte[] bPassword = Encoding.UTF8.GetBytes(password);
				byte[] bSalt = user.Salt;
				byte[] bHash = Crypt.Instance.Hash(bPassword, bSalt);

				if (user.Hash.SequenceEqual(bHash))
				{
					AuthLogger.Instance.PasswordSuccess(user.Email, user.UserID);
				}
				else
				{
					AuthLogger.Instance.PasswordFail(user.Email, user.UserID);
					return AuthResult.PasswordIncorrect;
				}

				try
				{
					db.Entry(user).Reference(usr => usr.UserKeyStore).Load();
					if (user.UserKeyStore == null)
					{
						user.UserKeyStore = KeyStore.DefaultDbKeyStore(password, bSalt, user.UserID);
						db.SaveChanges();
					}
					TempKeyStore = new KeyStore(user.UserKeyStore, password, bSalt);
					return AuthResult.Success;

				} catch (KeyStoreException) { 
					return AuthResult.KeyStoreInvalid;
				}
			}
			throw new AuthException("Login has no result, database failure might have occured.");
		}

		internal AuthResult UpdatePassword(string oldpass, string newpass)
		{
			using (DataContext db = new DataContext())
			{
				Users u = Users.FindByEmail(this.Email, db);
				if (u == null)
					return AuthResult.UserNotFound;

				AuthResult oldpwres = ValidateLogin(oldpass);
				if (oldpwres != AuthResult.Success)
					return oldpwres;

				db.Entry(u).Reference(usr => usr.UserKeyStore).Load();

				// Get the user key store and decrypt their private key
				UserKeyStore uks = u.UserKeyStore;
				byte[] aesKey = Crypt.Instance.DeriveKey(oldpass, u.Salt, uks.DesIv);
				byte[] rsaPrivBytes = Crypt.Instance.DecryptAes(uks.RsaPriv, aesKey, uks.AesIv);

				// Do the password update
				u.UpdatePassword(newpass);

				// Encrypt the private key again
				byte[] newAesIv = Crypt.Instance.Generate(16);
				uks.AesIv = newAesIv;
				byte[] newAesKey = Crypt.Instance.DeriveKey(newpass, u.Salt, uks.DesIv);
				byte[] newRsaPrivEnc = Crypt.Instance.EncryptAes(rsaPrivBytes, newAesKey, newAesIv);
				uks.RsaPriv = newRsaPrivEnc;

				db.SaveChanges();

				AuthLogger.Instance.PasswordFail(u.Email, u.UserID);
				return AuthResult.Success;
			}
		}

		// Returns a string array containing the user's roles
		internal string[] UserRoles
		{
			get
			{
				using (DataContext db = new DataContext())
				{
					Users u = Users.FindByEmail(this.Email, db);
					if (u == null)
						return new string[0];
					switch (u.AccessLevel)
					{
						case AccessLevel.Admin:
							return new string[] { "Admin", "Staff", "User" };
						case AccessLevel.Staff:
							return new string[] { "Staff", "User" };
						case AccessLevel.User:
							return new string[] { "User" };
						default:
							return new string[0];
					}

				}
			}
		}

		internal HttpCookie AuthCookie
		{
			get
			{
				Users u = Users.FindByEmail(this.Email);
				FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
						version: 1,
						name: this.Email,
						issueDate: DateTime.Now,
						expiration: DateTime.Now.AddMinutes(HttpContext.Current.Session.Timeout),
						isPersistent: false,
						userData: u.UserID + ";" + u.UserName + ";" + (int)u.AccessLevel
				);

				string encryptedTicket = FormsAuthentication.Encrypt(ticket);
				HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

				return cookie;
			}
		}

		/*
		 * 2FA CONF INSTANCE METHODS
		 */
		internal bool Is2FAEnabled
		{
			get
			{
				using (DataContext db = new DataContext())
				{
					Users u = Users.FindByEmail(this.Email, db);
					string sec = GetTotpSecret(db);
					if (!string.IsNullOrEmpty(sec))
						return true;
					else
						return false;
				}
			}
		}

		internal OtpResult Validate2FA(string totp, string base32sec = null)
		{
			if (base32sec == null)
				using (DataContext db = new DataContext())
				{
					base32sec = GetTotpSecret(db);
				}
			using (OTPTool ot = new OTPTool(base32sec))
			{
				return ot.Validate(totp);
			}
			throw new Exception("TOTP Generation Exception");
		}

		internal bool Set2FASecret(string b32sec)
		{
			if (b32sec != null && b32sec.Length != 16)
			{
				Debug.WriteLine("Malformed base32 secret length: " + b32sec.Length);
				return false;
			}

			using (DataContext db = new DataContext())
			{
				if (!Global.IsInUnitTest)
				if (b32sec == null)
					AuthLogger.Instance.TOTPDisabled();
				else
					AuthLogger.Instance.TOTPChanged();

				Users u = Users.FindByEmail(Email, db);
				db.Entry(u).Reference(usr => usr.UserKeyStore).Load();

				if (u.UserKeyStore != null)
					u.UserKeyStore.TOTPSecret = b32sec;
				else
					throw new KeyStoreException("Key store does not exist");

				db.SaveChanges();
				return true;
			}
		}

		/*
		 *  UserKeyStore related methods
		 */

		internal KeyStore GetKeyStore(DataContext db, string password, byte[] salt)
		{
			Users u = Users.FindByEmail(Email, db);
			db.Entry(u).Reference(usr => usr.UserKeyStore).Load();

			// Convert to object from data layer
			KeyStore ks = new KeyStore(u.UserKeyStore, password, salt);

			return ks;
		}

		internal string GetTotpSecret(DataContext db)
		{
			Users u = Users.FindByEmail(Email, db);
			db.Entry(u).Reference(usr => usr.UserKeyStore).Load();

			if (!(u.UserKeyStore is UserKeyStore))
				throw new KeyStoreException("UserKeyStore is not valid");

			return u.UserKeyStore.TOTPSecret;
		}

		internal string GetRsaPublic(string email = null)
		{
			using (DataContext db = new DataContext())
			{
				Users u;
				if (email == null)
					u = Users.FindByEmail(this.Email, db);
				else
					u = Users.FindByEmail(email, db);
				db.Entry(u).Reference(usr => usr.UserKeyStore).Load();
				return u.UserKeyStore.RsaPub;
			}
		}

		/*
		 *  Authentication STATIC methods
		 */
		internal static bool PasswordStrong(string password)
		{
			if (password.Length < 8)
				return false;
			if (!Regex.IsMatch(password, "^[a-zA-Z0-9]*$"))
				return false;
			return true;

		}


		// User creation method (adds to database)
		internal static RegisterResult CreateUser(string email, string username, string fullname, string password, DateTime birthdate)
		{
			if (Users.FindByEmail(email) != null)
				return RegisterResult.EmailTaken;

			Users u = new Users()
			{
				Email = email,
				UserName = username,
				FullName = fullname,
				BirthDate = birthdate,
				Registered = DateTime.Now,
				LastLogin = DateTime.Now,
				Coins = 0,
				ByteDollars = 0,
				TotalExp = 0,
				AccessLevel = AccessLevel.User
			};

			u.UpdatePassword(password);

			using (DataContext db = new DataContext())
			{
				db.Users.Add(u);
				db.SaveChanges();
				Debug.WriteLine("User creation attempted");

				Users createduser = Users.FindByEmail(email, db);
				createduser.UserKeyStore = KeyStore.DefaultDbKeyStore(password, createduser.Salt, createduser.UserID);

				if (createduser is Users)
				{
					using (MailClient mc = new MailClient(createduser.Email))
					{
						mc.Subject = "Verify your Email Address";
						mc.AddLine("Thank you for registering with HackNet!");
						mc.AddLine("We hope you enjoy your gaming experience with us,");
						mc.AddLine("Kindly verify your email address by clicking on the link below");
						mc.Send(createduser.FullName, "Verify Email", "https://haxnet.azurewebsites.net/");
						// TODO: Actual email verification (WL)
					}
					Machines.DefaultMachine(createduser, db);
					ItemLogic.StoreDefaultParts(db, u.UserID);
					AuthLogger.Instance.UserRegistered(u.Email, u.UserID);
					return RegisterResult.Success;
				}
				else
				{
					throw new RegistrationException("User cannot be registered due to an error (NOT_TYPE_USER)");
				}
			}
		}

		// Check if authenticated
		internal static bool IsAuthenticated(string email = null)
		{
			if (Global.IsInUnitTest)
				return true;

			if (HttpContext.Current == null)
				throw new AuthException("Current HTTP Context is NULL");

			if (email == null)
			{
				return HttpContext.Current.User.Identity.IsAuthenticated;
			}
			else
			{
				return (HttpContext.Current.User.Identity.Name.Equals(email.ToLower()));
			}
		}

		/// <summary>
		/// Gives UserID from Username input
		/// </summary>
		/// <param name="username">The user's username</param>
		/// <returns>The user's UserId</returns>
		internal static int ConvertUsernameToId(string username)
		{
			Users u = Users.FindByUsername(username);
			if (u == null)
				return -1;
			else
				return u.UserID;
		}


		/// <summary>
		/// Gives Username from UserID input
		/// </summary>
		/// <param name="id">The user's userid</param>
		/// <returns>The user's username</returns>
		internal static string ConvertIdToUsername(int id)
		{
			using (DataContext db = new DataContext())
			{
				Users u = db.Users.Find(id);
				if (u == null)
					return null;
				else
					return u.UserName;
			}
		}

		// Get email of authenticated user
		internal static string GetEmail()
		{

			if (!IsAuthenticated())
				throw new AuthException("User is not logged in");

			return HttpContext.Current.User.Identity.Name;
		}

		internal static int GetUserId()
		{

			string[] UserData = GetUserData();
			int UserId;
			if (!int.TryParse(UserData[0], out UserId))
				throw new AuthException("UserId is not an integer");

			return UserId;
		}

		internal static string GetUserName()
		{

			string[] UserData = GetUserData();

			return UserData[1];
		}

		internal static AccessLevel GetAccessLevel()
		{
			if (Global.IsInUnitTest)
				return AccessLevel.User;

			string[] UserData = GetUserData();
			int AccessLevelNum;
			if (!int.TryParse(UserData[2], out AccessLevelNum))
				throw new AuthException("AccessLevel is not an integer");

			if (!Enum.IsDefined(typeof(AccessLevel), (AccessLevel)AccessLevelNum))
				throw new AuthException("Invalid Access Level" + AccessLevelNum);

			return (AccessLevel)AccessLevelNum;
		}

		private static string[] GetUserData()
		{
			if (Global.IsInUnitTest)
				return new string[] { "6", "UnitTester" , "hacknet@wlgoh.com" };

			if (!IsAuthenticated())
				throw new AuthException("User is not logged in");

			FormsIdentity ident = HttpContext.Current.User.Identity as FormsIdentity;
			if (ident == null)
				throw new AuthException("Forms Authentication Identity is not authenticated");

			string[] UserData = ident.Ticket.UserData.Split(';');

			if (UserData.Length != 3)
				throw new AuthException("UserData is of incorrect length");

			return UserData;
		}


		internal static Users GetCurrentUser(bool ReadOnly = true, DataContext db = null)
		{
			string email = GetEmail();

			if (ReadOnly == false && db == null)
			{
				Debug.WriteLine("GetCurrentUser raised an error!");
				throw new ArgumentNullException("DataContext cannot be null if it is not read-only");
			}

			if (ReadOnly == true)
				using (DataContext db1 = new DataContext())
				{
					Users user = (from u in db1.Users
								  where u.Email == email
								  select u).FirstOrDefault();
					if (user != null)
						return user;
					else
						throw new UserException("User not found");
				}
			else
			{
				Users user = (from u in db.Users
							  where u.Email == email
							  select u).FirstOrDefault();
				if (user != null)
					return user;
				else
					throw new UserException("User not found");
			}


		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~Authenticate() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		void IDisposable.Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

	}

	internal enum AuthResult
	{
		Success = 0,
		UserNotFound = 1,
		PasswordIncorrect = 2,
		EmailNotVerified = 3,
		OtherError = 4,
		KeyStoreInvalid = 5
	}

	internal enum RegisterResult
	{
		Success = 0,
		UsernameTaken = 1,
		EmailTaken = 2,
		ValidationException = 3,
		OtherException = 4
	}

	public enum OtpResult
	{
		Success = 0,
		WrongOtp = 1,
		WrongLength = 2,
		NotInt = 3
	}
}