using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;

namespace HackNet.Security
{
    public class Authenticate : IDisposable
    {
        internal string Hash(byte[] password, byte[] salt = null)
        {
			// Start the stopwatch
			Stopwatch sw = new Stopwatch();
			sw.Start();
            byte[] hashedbytes;
			// If no salt specified, use default salt value
            if (salt == null)
                salt = Convert.FromBase64String("DefaultSalt=");
			// RFC2898 Implements HMAC Based SHA1, which is FIPS Compliant
            using (var kdf = new Rfc2898DeriveBytes(password, salt, 999))
                hashedbytes = kdf.GetBytes(128);
			// Stop the stopwatch
			sw.Stop();
			Debug.WriteLine(sw.Elapsed);
			// Return the hash
            return Convert.ToBase64String(hashedbytes);
        }

		public string SHA512Hash(string plaintext, byte[] salt = null)
		{
			// Obtain base variables
			byte[] ptBytes = Encoding.UTF8.GetBytes(plaintext);
			byte[] combinedBytes;
			string newHash;

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
				newHash = Convert.ToBase64String(shaCalc.ComputeHash(combinedBytes));
			}

			// Return the hash
			return newHash;

		}


		// Static utility methods
		internal static byte[] Encode64(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        internal static string Decode64(byte[] arr)
        {
            return Encoding.UTF8.GetString(arr);
        }

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

        internal static string GetEmail()
        {
            if (!IsAuthenticated())
                throw new AuthException("Not logged in");

            return HttpContext.Current.User.Identity.Name;
        }

        internal static byte[] Generate(int size)
        {
            if (size == 0) // Guard clause
                return null;
            byte[] random = new byte[size];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(random);
            return random;
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