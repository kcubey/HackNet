using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HackNet.Security
{
    public class Authenticate
    {
        internal static string Hash(string password, string salt = null)
        {
            using (SHA512 s512 = SHA512.Create())
            {
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, s512, CryptoStreamMode.Write);

            }
            return password;
        }

        internal static byte[] Generate(int length)
        {
            if (length == 0) // Guard clause
                return null;
            byte[] random = new byte[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                rng.GetBytes(random);
            return random;
        }

    }
}