using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HackNet.Security
{
    public class Authenticate : IDisposable
    {
        internal string Hash(byte[] password, byte[] salt = null)
        {
            byte[] hashedbytes;
            if (salt == null)
            {
                salt = Convert.FromBase64String("DefaultSalt=");
            }
           
            using (var kdf = new Rfc2898DeriveBytes(password, salt, 999))
            {
                hashedbytes = kdf.GetBytes(128);
            }

            return Convert.ToBase64String(hashedbytes);
        }

        internal byte[] Encode(string str)
        {
            return Convert.FromBase64String(str);
        }

        internal byte[] Generate(int size)
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