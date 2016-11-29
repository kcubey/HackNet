using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;

using System.Web.Security;
using static HackNet.Security.Authenticate;

namespace HackNet.Auth {
	public partial class SignIn : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e)
		{
            DataContext ctx = new DataContext();
			Msg.Text = "Sign in with your Google Drive email and password as 123";
        }

        protected void LoginClick(object sender, EventArgs e)
        {
			using (Authenticate auth = new Authenticate())
            {
				LoginResult result = auth.ValidateLogin(Email.Text, UserPass.Text);
				switch(result)
				{
					case (LoginResult.Success):
						FormsAuthentication.RedirectFromLoginPage(Email.Text, false);
						break;
					case (LoginResult.PasswordIncorrect):
						Msg.Text = "User and/or password not found (1)";
						break;
					case (LoginResult.UserNotFound):
						Msg.Text = "User and/or password not found (2)";
						break;
					default:
						Msg.Text = "Unhandled error has occured";
						break;
				}

				// Privileged Execution

			}
        }

		protected void BypassClick(object sender, EventArgs e)
		{
			FormsAuthentication.RedirectFromLoginPage("Bypasser", false);
		}
	}
}