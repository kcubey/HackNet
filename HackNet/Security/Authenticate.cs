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

	/// <summary>
	/// HackNet's Authentication and Security Tools
	/// </summary>
	internal class Authenticate : IDisposable
	{
		internal string Email { get; private set; }

		internal KeyStore TempKeyStore { get; private set; }

		Stopwatch sw = new Stopwatch();

		/// <summary>
		/// Constructor for the authenticate class, should be used for authenticated HttpContexts only
		/// </summary>
		internal Authenticate()
		{
			Email = GetEmail();
			Debug.WriteLine("Creating new authenticate instance for " + Email);
		}

		/// <summary>
		/// Constructor for the authenticate class, creates an authenticate entity for a user
		/// </summary>
		/// <param name="email">The user's email address</param>
		internal Authenticate(string email)
		{
			// To ensure email casing is correct
			Email = Users.FindByEmail(email: email).Email;
			Debug.WriteLine("Creating new authenticate instance for " + Email);
		}


		/// <summary>
		/// Validate the user's password
		/// </summary>
		/// <param name="password">The user's password</param>
		/// <param name="checkEmailValidity">Whether to check if the email address is verified</param>
		/// <returns></returns>
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

		/// <summary>
		/// Update the user's password
		/// </summary>
		/// <param name="oldpass">Old password to validate</param>
		/// <param name="newpass">Intended password to be changed</param>
		/// <returns>AuthResult Success if password is changed successfully</returns>
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
				byte[] newAesIv = Crypt.Instance.GenerateIv("AES");
				uks.AesIv = newAesIv;
				byte[] newAesKey = Crypt.Instance.DeriveKey(newpass, u.Salt, uks.DesIv);
				byte[] newRsaPrivEnc = Crypt.Instance.EncryptAes(rsaPrivBytes, newAesKey, newAesIv);
				uks.RsaPriv = newRsaPrivEnc;

				db.SaveChanges();

				AuthLogger.Instance.PasswordChanged();
				return AuthResult.Success;
			}
		}

		/// <summary>
		/// Returns a string array containing the user's roles
		/// </summary>
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

		/// <summary>
		/// Generates an authentication cookie for this user
		/// </summary>
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

		/// <summary>
		/// Checks if 2FA has been enabled by the user
		/// </summary>
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

		/// <summary>
		/// Validates the user's 2FA input
		/// </summary>
		/// <param name="totp">The 2FA code entered</param>
		/// <param name="base32sec">OPTIONAL: Base32 secret to check against</param>
		/// <returns></returns>
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

		/// <summary>
		/// Sets the 2FA secret for this user
		/// </summary>
		/// <param name="b32sec">The Base32 secret to set</param>
		/// <returns></returns>
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

		/// <summary>
		/// Get the user's decrypted KeyStore
		/// </summary>
		/// <param name="db">DataContext Entity</param>
		/// <param name="password">Password of the current user</param>
		/// <param name="salt">Salt of the user's password</param>
		/// <returns>Decrypted keystore</returns>
		internal KeyStore GetKeyStore(DataContext db, string password, byte[] salt)
		{
			Users u = Users.FindByEmail(Email, db);
			db.Entry(u).Reference(usr => usr.UserKeyStore).Load();

			// Convert to object from data layer
			KeyStore ks = new KeyStore(u.UserKeyStore, password, salt);

			return ks;
		}

		/// <summary>
		/// Gets the 2FA Secret of this user
		/// </summary>
		/// <param name="db">DataContext entity for database connections</param>
		/// <returns>2FA Secret of this user</returns>
		internal string GetTotpSecret(DataContext db)
		{
			Users u = Users.FindByEmail(Email, db);
			db.Entry(u).Reference(usr => usr.UserKeyStore).Load();

			if (!(u.UserKeyStore is UserKeyStore))
				throw new KeyStoreException("UserKeyStore is not valid");

			return u.UserKeyStore.TOTPSecret;
		}

		/// <summary>
		/// Gets the encryption (public) RSA key of this user
		/// </summary>
		/// <param name="db">DataContext entity</param>
		/// <param name="email">OPTIONAL: The user's email address</param>
		/// <returns>User's RSA Encryption key</returns>
		internal string GetRsaPublic(DataContext db, int userid = -1)
		{
			Users u;

			if (userid == -1)
				u = Users.FindByEmail(Email, db);
			else
				u = db.Users.Find(userid);

			db.Entry(u).Reference(usr => usr.UserKeyStore).Load();
			return u.UserKeyStore.RsaPub;
		}

		/// <summary>
		/// STATIC: Checks if password given meets the minimum strength requirements
		/// </summary>
		/// <param name="password">Password to check for</param>
		/// <returns></returns>
		internal static bool PasswordStrong(string password)
		{
			if (password.Length < 8)
				return false;
			if (!Regex.IsMatch(password, "^[a-zA-Z0-9]*$"))
				return false;
			return true;

		}


		/// <summary>
		/// Creates a user and adds to database
		/// </summary>
		/// <returns>Result of registration</returns>
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

		/// <summary>
		/// Checks if the current user is authenticated
		/// </summary>
		/// <param name="email">Checks if the authenticated user has this email</param>
		/// <returns>Whether the current user is authenticated</returns>
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

		/// <summary>
		/// Gets the email address of the current user, no DB required
		/// </summary>
		/// <returns></returns>
		internal static string GetEmail()
		{

			if (!IsAuthenticated())
				throw new AuthException("User is not logged in");

			return HttpContext.Current.User.Identity.Name;
		}

		/// <summary>
		/// Gets the User ID of the current user, no DB required
		/// </summary>
		/// <returns></returns>
		internal static int GetUserId()
		{

			string[] UserData = GetUserData();
			int UserId;
			if (!int.TryParse(UserData[0], out UserId))
				throw new AuthException("UserId is not an integer");

			return UserId;
		}

		/// <summary>
		/// Gets the username of the current user, no DB required
		/// </summary>
		/// <returns></returns>
		internal static string GetUserName()
		{

			string[] UserData = GetUserData();

			return UserData[1];
		}

		/// <summary>
		/// Gets the AccessLevel enum of the current user, no DB required
		/// </summary>
		/// <returns></returns>
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

		/// <summary>
		/// PRIVATE: Read the user data and parse into string array
		/// </summary>
		/// <returns></returns>
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


		/// <summary>
		/// Get the user entity of the current logged in user
		/// </summary>
		/// <param name="ReadOnly">OPTIONAL: Whether to enable EF tracking on the user</param>
		/// <param name="db">OPTIONAL: If not readonly, provide database context</param>
		/// <returns></returns>
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

	/// <summary>
	/// Authentication result enum
	/// </summary>
	internal enum AuthResult
	{
		Success = 0,
		UserNotFound = 1,
		PasswordIncorrect = 2,
		EmailNotVerified = 3,
		OtherError = 4,
		KeyStoreInvalid = 5
	}

	/// <summary>
	/// Registration result enum
	/// </summary>
	internal enum RegisterResult
	{
		Success = 0,
		UsernameTaken = 1,
		EmailTaken = 2,
		ValidationException = 3,
		OtherException = 4
	}

	/// <summary>
	/// One-time password validation result
	/// </summary>
	public enum OtpResult
	{
		Success = 0,
		WrongOtp = 1,
		WrongLength = 2,
		NotInt = 3
	}
}