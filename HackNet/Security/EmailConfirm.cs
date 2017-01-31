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

			if (u.AccessLevel != AccessLevel.Unconfirmed)
				return true;
			else
				return false;
		}

		public static EmailConfirmResult EmailValidate(string email, string code)
		{
			if (code == null)
				throw new ArgumentNullException("Code cannot be null");

			using (DataContext db = new DataContext())
			{
				Users usr = db.Users.Where(u => u.Email == email).FirstOrDefault();

				if (usr == null)
					return EmailConfirmResult.UserNotFound;

				List<Confirmations> confirms =
								db.Confirmations
								.Where(c => c.Email == email)
								.ToList();

				foreach (var c in confirms)
				{
					if (c.Expiry > DateTime.Now)
					{
						if (c.Type == ConfirmType.EmailConfirm && c.Code == code)
						{
							c.Code = null;
							usr.AccessLevel = AccessLevel.User;
							db.SaveChanges();
							return EmailConfirmResult.Success;
						}
					}
				}

				return EmailConfirmResult.Failed;
			}
		}

		private static string GenerateString()
		{
			byte[] strBytes = new byte[12];
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
		UserNotFound = 2,
		OtherError = 3
	}
}