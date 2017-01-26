using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HackNet.Security
{
	public class EmailConfirm
	{
		public static string GenerateString()
		{
			byte[] strBytes = new byte[16];
			using (RNGCryptoServiceProvider rngcsp = new RNGCryptoServiceProvider())
			{
				rngcsp.GetBytes(strBytes);
			}
			return Convert.ToBase64String(strBytes);
		}

		public static bool ValidateCode(string email, string code)
		{
			return true;
			// TODO:
		}
	}
}