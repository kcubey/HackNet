using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;
using HackNet.Data;
using System.Data.Entity.Core;
using System.Text.RegularExpressions;

namespace HackNet.Security
{
	internal class Authenticate : IDisposable
	{
		Stopwatch sw = new Stopwatch();
		internal Authenticate()
		{

		}

		internal AuthResult ValidateLogin(string email, string password, bool checkEmailValidity = true)
		{
			using (DataContext db = new DataContext())
			{
				Users user = Users.FindEmail(email, db);

				if (user == null)
					return AuthResult.UserNotFound;

				if (checkEmailValidity && user.AccessLevel == AccessLevel.Unverified)
					return AuthResult.EmailNotVerified;

				byte[] bPassword = Encoding.UTF8.GetBytes(password);

				if (user.Hash.SequenceEqual(Hash(bPassword, user.Salt)))
					return AuthResult.Success;
				else
					return AuthResult.PasswordIncorrect;
			}
			throw new AuthException("Error connecting to database");
		}

		internal AuthResult UpdatePassword(string email, string oldpass, string newpass)
		{
			using (DataContext db = new DataContext())
			{
				Users user = Users.FindEmail(email, db);
				if (user == null)
					return AuthResult.UserNotFound;
				if (user.UpdatePassword(newpass, oldpass))
				{
					db.SaveChanges();
					return AuthResult.Success;
				}
				else
					return AuthResult.PasswordIncorrect;
			}
		}

		internal RegisterResult CreateUser(string email, string username, string fullname, string password, DateTime birthdate)
		{
			if (Users.FindEmail(email) != null)
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

				Users createduser = Users.FindEmail(email, db);
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
					return RegisterResult.Success;
				}
			}

			return RegisterResult.OtherException;
		}

		internal bool PasswordStrong(string password)
		{
			if (password.Length < 8)
				return false;
			if (!Regex.IsMatch(password, "^[a-zA-Z0-9]*$"))
				return false;
			return true;

		}

		internal byte[] Hash(byte[] password, byte[] salt = null)
		{
			// Start the stopwatch
			Stopwatch sw = new Stopwatch();
			sw.Start();
			byte[] hashedbytes;
			// If no salt specified, use default salt value
			if (salt == null)
				salt = Convert.FromBase64String("DefaultSalt=");
			// RFC2898 Implements HMAC Based SHA1, which is FIPS Compliant
			using (var kdf = new Rfc2898DeriveBytes(password, salt, 5000))
				hashedbytes = kdf.GetBytes(128);
			// Stop the stopwatch
			sw.Stop();
			Debug.WriteLine(sw.Elapsed);
			// Return the hash
			return hashedbytes;
		}

		public byte[] SHA512Hash(string plaintext, byte[] salt = null)
		{
			// Obtain base variables
			byte[] ptBytes = Encoding.UTF8.GetBytes(plaintext);
			byte[] combinedBytes;
			byte[] newHash;

			// If salt is present, append it to plaintext
			if (salt == null)
			{
				combinedBytes = new byte[ptBytes.Length];
				ptBytes.CopyTo(combinedBytes, 0);
			}
			else
			{
				combinedBytes = new byte[ptBytes.Length + salt.Length];
				ptBytes.CopyTo(combinedBytes, 0);
				salt.CopyTo(combinedBytes, ptBytes.Length);
			}

			// Do the hashing
			using (SHA512 shaCalc = new SHA512Managed())
			{
				newHash = shaCalc.ComputeHash(combinedBytes);
			}

			// Return the hash
			return newHash;

		}


		// Static utility methods
		internal static byte[] Encode64(string str)
		{

			return Convert.FromBase64String(str);
		}

		internal static string Decode64(byte[] arr)
		{
			return Convert.ToBase64String(arr);
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
				throw new ArgumentNullException("DataContext cannot be null if it is not read-only");
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

		// Generate bytes from RNGCryptoServiceProvider
		internal static byte[] Generate(int size)
		{
			if (size == 0) // Guard clause
				return null;
			byte[] random = new byte[size];
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
				rng.GetBytes(random);
			return random;
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
}