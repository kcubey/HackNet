using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace HackNet.Security
{
	public static class EmailConfirm
	{
		public static void SendEmailForConfirmation(Users u)
		{
			string code = GenerateString();
			string link = string.Format("https://haxnet.azurewebsites.net/?Email={0}&Code={1}", u.Email, code);
			using (MailClient mc = new MailClient(u.Email))
			{
				mc.Subject = "Verify your Email Address";
				mc.AddLine("Thank you for registering with HackNet!");
				mc.AddLine("We hope you enjoy your gaming experience with us,");
				mc.AddLine("Kindly verify your email address by clicking on the link below");
				mc.AddLine("If that does not work, please use this code: " + code);
				mc.Send(u.FullName, "Verify Email", link);
			}
		}

		public static bool IsEmailValidated(Users u)
		{
			if (u == null)
				return false;

			if (u.EmailConfirmation == null)
				return true;
			else
				return false;
		}

		public static EmailConfirmResult ValidateCode(string email, string code)
		{
			using (DataContext db = new DataContext())
			{
				Users u = Users.FindByEmail(email, db);

				if (u == null)
				{
					return EmailConfirmResult.UserNotFound;
				}
				else if (u.EmailConfirmation == null)
				{
					return EmailConfirmResult.AlreadyConfirmed;
				}
				else if (!code.Equals(u.EmailConfirmation))
				{
					return EmailConfirmResult.Failed;
				}
				else if (code.Equals(u.EmailConfirmation))
				{
					u.EmailConfirmation = null;
					return EmailConfirmResult.Success;
				}
				else
				{
					return EmailConfirmResult.OtherError;
				}
			}
		}

		private static string GenerateString()
		{
			byte[] strBytes = new byte[16];
			using (RNGCryptoServiceProvider rngcsp = new RNGCryptoServiceProvider())
			{
				rngcsp.GetBytes(strBytes);
			}
			return Convert.ToBase64String(strBytes);
		}

	}

	public enum EmailConfirmResult
	{
		Success = 0,
		Failed = 1,
		AlreadyConfirmed = 2,
		UserNotFound = 3,
		OtherError = 4
	}
}