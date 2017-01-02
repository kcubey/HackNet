using System;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Text;
using HackNet.Data;
using System.Text.RegularExpressions;
using HackNet.Loggers;
using System.Collections.Generic;
using System.Web.Security;

namespace HackNet.Security
{
	internal class Authenticate : IDisposable
	{
		internal string Email { get; private set; } 
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
				byte[] bHash = Crypt.Instance.Hash(bPassword, user.Salt);
				if (user.Hash.SequenceEqual(bHash))
				{
					AuthLogger.Instance.PasswordSuccess(Email);
					return AuthResult.Success;
				}
				else
				{
					AuthLogger.Instance.PasswordFail(Email);
					return AuthResult.PasswordIncorrect;
				}
			}
			throw new AuthException("Error connecting to database");
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
				u.UpdatePassword(newpass);
				db.SaveChanges();
				AuthLogger.Instance.PasswordFail(Email);
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
				FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
						version: 1,
						name: this.Email,
						issueDate: DateTime.Now,
						expiration: DateTime.Now.AddSeconds(HttpContext.Current.Session.Timeout),
						isPersistent: false,
						userData: roles
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
					string sec = GetKeyStore(db).TOTPSecret;
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
					var u = Users.FindByEmail(Email, db);
					var uks = GetKeyStore(db);
					base32sec = uks.TOTPSecret;
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
			using (DataContext db = new DataContext()) {
				if (b32sec == null)
					AuthLogger.Instance.TOTPDisabled(Email);
				else
					AuthLogger.Instance.TOTPChanged(Email);
				GetKeyStore(db).TOTPSecret = b32sec;
				db.SaveChanges();
				return true;
			}
		}

		/*
		 *  UserKeyStore related methods
		 */

		internal UserKeyStore GetKeyStore(DataContext db)
		{
			Users u = Users.FindByEmail(Email, db);
			db.Entry(u).Reference(usr => usr.UserKeyStore).Load();
			if (u.UserKeyStore == null)
			{
				UserKeyStore uks = new UserKeyStore
				{
					RsaPub = null,
					RsaPriv = new byte[0],
					TOTPSecret = null,
					UserId = u.UserID
				};
				db.UserKeyStore.Add(uks);
				db.SaveChanges();
			}
			return u.UserKeyStore;
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
				ByteDollars = 0
			};
			u.UpdatePassword(password);

			using (DataContext db = new DataContext())
			{
				db.Users.Add(u);
				db.SaveChanges();
				Debug.WriteLine("User creation attempted");

				Users createduser = Users.FindByEmail(email, db);
				if (createduser != null)
				{
					using (MailClient mc = new MailClient(createduser.Email))
					{
						mc.Subject = "Verify your Email Address";
						mc.AddLine("Thank you for registering with HackNet!");
						mc.AddLine("We hope you enjoy your gaming experience with us,");
						mc.AddLine("Kindly verify your email address by clicking on the link below");
						mc.Send(createduser.FullName, "Verify Email", "https://haxnet.azurewebsites.net/");
					}
					AuthLogger.Instance.UserRegistered(email);
					return RegisterResult.Success;
				}
			}

			return RegisterResult.OtherException;
		}

		// Check if authenticated
		internal static bool IsAuthenticated(string email = null)
		{
			if (email == null)
			{
				return HttpContext.Current.User.Identity.IsAuthenticated;
			}
			else
			{
				return (HttpContext.Current.User.Identity.Name.Equals(email.ToLower()));
			}
		}

		// Get email of authenticated user
		internal static string GetEmail()
		{
			if (!IsAuthenticated())
				throw new AuthException("User is not logged in");

			return HttpContext.Current.User.Identity.Name;
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
		OtherError = 4
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