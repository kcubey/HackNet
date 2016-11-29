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

namespace HackNet.Security
{
    internal class Authenticate : IDisposable
    {
		Stopwatch sw = new Stopwatch();
		internal Authenticate()
		{

		}

		internal LoginResult ValidateLogin(string email, string password)
		{
			using (DataContext db = new DataContext())
			{
				Users user;
				try {
					user = (from u in db.Users
							where u.Email == email
							select u).FirstOrDefault();
				} catch (EntityCommandExecutionException)
				{
					throw new ConnectionException("Database link failure has occured");
				}

				if (user == null)
					return LoginResult.UserNotFound;

				byte[] bSalt = user.Salt;
				byte[] bPassword = Encoding.UTF8.GetBytes(password);
				byte[] bHashed = Hash(bPassword, bSalt);
				byte[] dbHash = user.Hash;
				if (bHashed.SequenceEqual(dbHash))
				{
					return LoginResult.Success;
				} else
				{
					return LoginResult.PasswordIncorrect;
				}
			}
				 
			throw new AuthException("Error connecting to database");
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
            } else
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

		internal static Users GetUser()
		{
			string email = GetEmail();
			using (DataContext db = new DataContext())
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

		internal enum LoginResult
		{
			Success = 0,
			UserNotFound = 1,
			PasswordIncorrect = 2,
			OtherError = 3
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