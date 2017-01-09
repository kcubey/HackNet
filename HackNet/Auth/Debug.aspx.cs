using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

using HackNet.Security;
using System.Text;
using HackNet.Data;

namespace HackNet.Auth
{
	public partial class Debug : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Msg5.Text = RsaTest("Some string for RSA encryption");
			if (Authenticate.IsAuthenticated())
			{
				UDetails1.Text = "User ID: " + Authenticate.GetUserId().ToString();
				UDetails2.Text = "Username: " + Authenticate.GetUserName();
				UDetails3.Text = "Access Level: " + Enum.GetName(typeof(AccessLevel), Authenticate.GetAccessLevel());
			}
		}

		protected string RsaTest(string somestring)
		{
			string keypair = Crypt.Instance.GenerateRsaParameters();
			System.Diagnostics.Debug.WriteLine(keypair);
			string pubonly = Crypt.Instance.RemovePrivateKey(keypair);
			System.Diagnostics.Debug.WriteLine(pubonly);

			string plainText = somestring;
			System.Diagnostics.Debug.WriteLine(somestring);

			byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
			byte[] cipherBytes = Crypt.Instance.EncryptRsa(plainBytes, pubonly);
			byte[] decipheredBytes = Crypt.Instance.DecryptRsa(cipherBytes, pubonly);
			string decipheredText = Encoding.UTF8.GetString(decipheredBytes);

			System.Diagnostics.Debug.WriteLine(decipheredText);


			return decipheredText;
		}

		protected void SendMail()
		{
			Users u = Authenticate.GetCurrentUser();
			using (MailClient mc = new MailClient(u.Email))
			{
				mc.Subject = "Verify your Email Address";
				mc.AddLine("Thank you for registering with HackNet!");
				mc.AddLine("We hope you enjoy your gaming experience with us,");
				mc.AddLine("Kindly verify your email address by clicking on the link below");
				mc.Send(u.FullName, "Verify Email", "https://haxnet.azurewebsites.net/");
			}
		}
	}
}