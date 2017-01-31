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
		public static void SendEmailForConfirmation(Users u, DataContext db)
		{
			string code = GenerateString();
			Confirmations c = new Confirmations()
			{
				Email = u.Email,
				UserId = u.UserID,
				Type = ConfirmType.PasswordReset,
				Code = code
			};
			db.Confirmations.Add(c);
			string link = string.Format("https://haxnet.azurewebsites.net/Auth/ConfirmEmail?Email={0}&Code={1}", u.Email, code);
			using (MailClient mc = new MailClient(u.Email))
			{
				mc.Subject = "Verify your Email Address";
				mc.AddLine("");
				mc.AddLine("Kindly verify your email address by clicking on the link below");
				mc.AddLine("If that does not work, please use this code: " + code);
				mc.Send(u.FullName, "Verify Email", link);
			}
			db.SaveChanges();
		}

		public static void SendEmailForPasswordReset(Users u, DataContext db)
		{
			string code = GenerateString();
			Confirmations c = new Confirmations()
			{
				Email = u.Email,
				UserId = u.UserID,
				Type = ConfirmType.PasswordReset,
				Code = code
			};
			db.Confirmations.Add(c);
			string link = string.Format("https://haxnet.azurewebsites.net/Auth/ResetPassword?Email={0}&Code={1}", u.Email, code);
			using (MailClient mc = new MailClient(u.Email))
			{
				mc.Subject = "Password Reset Request";
				mc.AddLine("");
				mc.AddLine("You have initiated a password reset request!");
				mc.AddLine("If it was you, please click the link below to continue");
				mc.AddLine("Otherwise, you can safely ignore this message");
				mc.Send(u.FullName, "ResetPassword", link);
			}
			db.SaveChanges();
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

				// Get all confirmations for this Email Address
				List<Confirmations> confirms = GetAllConfirmations(email, db);

				foreach (var c in confirms)
				{
					if (c.Expiry > DateTime.Now && c.Type == ConfirmType.EmailConfirm && c.Code == code)
					{
						c.Code = null;
						usr.AccessLevel = AccessLevel.User;
						db.SaveChanges();
						return EmailConfirmResult.Success;
					}
				}

				return EmailConfirmResult.Failed;
			}
		}

		public static EmailConfirmResult ValidatePasswordReset(string email, string code)
		{
			if (code == null)
				throw new ArgumentNullException("Code cannot be null");

			using (DataContext db = new DataContext())
			{
				Users usr = db.Users.Where(u => u.Email == email).FirstOrDefault();

				if (usr == null)
					return EmailConfirmResult.UserNotFound;

				// Get all confirmations for this Email Address
				List<Confirmations> confirms = GetAllConfirmations(email, db);

				foreach (var c in confirms)
				{
					if (c.Expiry > DateTime.Now && c.Type == ConfirmType.PasswordReset && c.Code == code)
					{
						c.Code = null;

						string password = GenerateString();
						usr.UpdatePassword(password);

						db.SaveChanges();
						return EmailConfirmResult.Success;
					}
				}

				return EmailConfirmResult.Failed;
			}
		}


		private static List<Confirmations> GetAllConfirmations(string email, DataContext db)
		{
			List<Confirmations> confirms =
							db.Confirmations
							.Where(c => c.Email == email)
							.ToList();

			return confirms;
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