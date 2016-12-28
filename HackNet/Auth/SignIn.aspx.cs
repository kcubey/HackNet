using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;
using HackNet.Loggers;

using System.Web.Security;

namespace HackNet.Auth {
	public partial class SignIn : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Authenticate.IsAuthenticated())
				Response.Redirect("~/Game/Home");
            DataContext ctx = new DataContext();
			Msg.Text = "Sign in with your Google Drive email and password as 123";
        }

        protected void LoginClick(object sender, EventArgs e)
        {
			string email = Email.Text.ToLower();
			using (Authenticate auth = new Authenticate(email))
            {
				AuthResult result = auth.ValidateLogin(UserPass.Text);
				switch(result)
				{
					case (AuthResult.Success):
						LoginSuccess(email);
						break;
					case (AuthResult.PasswordIncorrect):
						AuthLogger.Instance.FailedLogin(email, "1.2.3.4");
						Msg.Text = "User and/or password not found (1)";
						break;
					case (AuthResult.UserNotFound):
						Msg.Text = "User and/or password not found (2)";
						break;
					default:
						Msg.Text = "Unhandled error has occured";
						break;
				}
			}
        }

		private void LoginSuccess(string email)
		{
			using (Authenticate a = new Authenticate(email))
			{
				if (a.Is2FAEnabled)
				{
					Session["PasswordSuccess"] = email;
					Response.Redirect("~/Auth/OtpVerify");
				} else
				{
					FormsAuthentication.SetAuthCookie(email, false);
					Response.Redirect("~/Default");
				}
			}
		}
	}
}