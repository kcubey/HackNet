using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;

using System.Web.Security;

namespace HackNet.Auth {
	public partial class SignIn : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e)
		{
            DataContext ctx = new DataContext();
			Msg.Text = "For actual user, please login with EMAIL 1@2.co and PASSWORD 123";
        }

        protected void LoginClick(object sender, EventArgs e)
        {
			using (Authenticate auth = new Authenticate())
            {
				auth.ValidateLogin(Email.Text, UserPass.Text);

                // Privileged Execution
                FormsAuthentication.RedirectFromLoginPage(Email.Text, false);

            }
        }

		protected void BypassClick(object sender, EventArgs e)
		{
			FormsAuthentication.RedirectFromLoginPage("Bypasser", false);
		}
	}
}