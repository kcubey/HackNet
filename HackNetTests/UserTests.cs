using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HackNet.Data;
using HackNet.Security;
using System.Web;
using System.Diagnostics;

namespace HackNetTests
{
	[TestClass]
	public class UserTests
	{

		[TestMethod]
		public void UserModify()
		{
			using (DataContext db = new DataContext())
			{
				Users u = Users.FindByEmail("hacknet@wlgoh.com", db);
				u.AccessLevel = AccessLevel.Staff;
				db.SaveChanges();

				u = Users.FindByEmail("hacknet@wlgoh.com", db);
				Assert.AreEqual(u.AccessLevel, AccessLevel.User);

				u.AccessLevel = AccessLevel.User;
				db.SaveChanges();
			}
		}

		[TestMethod]
		public void UserLogin()
		{
			using (Authenticate a = new Authenticate("hacknet@wlgoh.com"))
			{
				string password = "Qwerty12345";
				AuthResult isSuccessful = a.ValidateLogin(password, true);
				Assert.IsTrue(isSuccessful == AuthResult.Success);
			}
		}

		[TestMethod]
		public void UserChangePW()
		{ 
			using (Authenticate a = new Authenticate("hacknet@wlgoh.com"))
			{
				AuthResult Result1 = a.UpdatePassword("Qwerty12345", "Asdfgh12345");
				AuthResult Result2 = a.ValidateLogin("Asdfgh12345");
				a.UpdatePassword("Asdfgh12345", "Qwerty12345");
				Assert.IsTrue(Result1 == AuthResult.Success && Result2 == AuthResult.Success);
			}
		}

		[TestMethod]
		public void UserOTP()
		{
			string otpsec;
			using (Authenticate a = new Authenticate("hacknet@wlgoh.com"))
			using (OTPTool otp = new OTPTool())
			{
				otpsec = otp.RandomiseSecret();
				a.Set2FASecret(otpsec);
			}

			using (OTPTool otp = new OTPTool())
			using (Authenticate a = new Authenticate("hacknet@wlgoh.com"))
			{
				otp.SecretBase32 = otpsec;
				string otpinput = otp.OTPNow.ToString("D6");
				OtpResult rslt = a.Validate2FA(otpinput);
				if (rslt != OtpResult.Success)
				{
					Trace.Write(otp.ToString());
				}
				Assert.IsTrue(rslt == OtpResult.Success);
			}
		}
	}
}
