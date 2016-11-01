using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HackNet.Security {
    public class Authenticate {

        internal static string Hash(string password, string salt = null) {

            using (SHA256 s256 = SHA256.Create())
            using (SHA512 s512 = SHA512.Create())  {
                


            }

            return password;
        }

    }
}