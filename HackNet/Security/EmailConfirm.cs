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
		public static bool IsEmailValidated(Users u)
		{
			if (u == null)
				return false;

			if (u.AccessLevel != AccessLevel.Unconfirmed)
				return true;
			else
				return false;
		}



		public static void SendEmailForConfirmation(Users u, DataContext db)
		{
			string code = GenerateString(encode: true);

			Confirmations c = new Confirmations()
			{
				Email = u.Email,
				UserId = u.UserID,
				Type = ConfirmType.EmailConfirm,
				Code = HttpUtility.UrlDecode(code),
				Expiry = DateTime.Now.AddMinutes(30d)
			};

			db.Confirmations.Add(c);

			string link = string.Format("https://haxnet.azurewebsites.net/Auth/ConfirmEmail?Email={0}&Code={1}", u.Email, code);

			using (MailClient mc = new MailClient(u.Email))
			{
				mc.Subject = "HackNet - Verify your Email Address";
				mc.AddLine("");
				mc.AddLine("Kindly verify your email address by clicking on the link below");
				mc.AddLine("This link will expire in 30 minutes");
				mc.AddLine("If that does not work, please use this code: " + code);
				mc.Send(u.FullName, "Verify Email", link);
			}
			db.SaveChanges();
		}

		public static void SendEmailForPasswordReset(string email)
		{
			using (DataContext db = new DataContext())
			using (Authenticate a = new Authenticate(email))
			{
				string code = GenerateString(encode: true);
				Confirmations c = new Confirmations()
				{
					Email = a.Email,
					UserId = a.UserId,
					Type = ConfirmType.PasswordReset,
					Code = HttpUtility.UrlDecode(code), // no need to encode for db
					Expiry = DateTime.Now.AddMinutes(30d)
				};
				db.Confirmations.Add(c);

				string link = string.Format("https://haxnet.azurewebsites.net/Auth/ResetPassword?Email={0}&Code={1}", a.Email, code);
				link = HttpUtility.HtmlAttributeEncode(link); // Encoding for QueryString

				using (MailClient mc = new MailClient(a.Email))
				{
					mc.Subject = "Password Reset Request";
					mc.AddLine("");
					mc.AddLine("You have initiated a password reset request!");
					mc.AddLine("If it was you, please click the link below to continue");
					mc.AddLine("Otherwise, you can safely ignore this message as it will expire in 30 minutes");
					mc.Send("user", "Reset Password", link);
				}
				db.SaveChanges();
			}
		}

		public static void SendNewPassword(string email, string password)
		{
			using (DataContext db = new DataContext())
			using (Authenticate a = new Authenticate(email))
			{
				using (MailClient mc = new MailClient(a.Email))
				{
					mc.Subject = "Password Reset Result";
					mc.AddLine("");
					mc.AddLine("Your new password is " + password);
					mc.AddLine("Please change it as soon as possible and remember it");
					mc.Send("user");
				}
				db.SaveChanges();
			}
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
				List<Confirmations> confirms = GetAllConfirmations(email, code, db);

				foreach (var c in confirms)
				{
					if (c.Expiry > DateTime.Now && c.Type == ConfirmType.EmailConfirm)
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
				Users u = db.Users.Where(uzr => uzr.Email == email).FirstOrDefault();

				if (u == null)
					return EmailConfirmResult.UserNotFound;

				// Get all confirmations for this Email Address
				List<Confirmations> confirms = GetAllConfirmations(email, code, db);

				foreach (var c in confirms)
				{
					if (c.Expiry > DateTime.Now && c.Type == ConfirmType.PasswordReset)
					{
						c.Code = null;

						string password = GenerateString(encode: false);

						MessageLogic.QuitAllConversations(u, db);

						u.UpdatePassword(password);
						db.Entry(u).Reference(usr => usr.UserKeyStore).Load();
						db.Entry(u.UserKeyStore).CurrentValues.SetValues(KeyStore.DefaultDbKeyStore(password, u.Salt, u.UserID));
						SendNewPassword(email, password);

						db.SaveChanges();
						return EmailConfirmResult.Success;
					}
				}

				return EmailConfirmResult.Failed;
			}
		}

		private static List<Confirmations> GetAllConfirmations(string email, string code, DataContext db)
		{
			List<Confirmations> confirms =
							db.Confirmations
							.Where(c => c.Email == email && c.Code == code)
							.ToList();

			return confirms;
		}

		private static string GenerateString(bool encode = true)
		{
			byte[] strBytes = new byte[12];
			using (RNGCryptoServiceProvider rngcsp = new RNGCryptoServiceProvider())
			{
				rngcsp.GetBytes(strBytes);
			}
			string str = Convert.ToBase64String(strBytes);

			if (encode == true)
				return HttpUtility.UrlEncode(str);
			else
				return str;
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