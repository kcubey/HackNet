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
		protected void Page_Load(object sender, EventArgs e) {
            Msg.Text = "IMPLEMENTING: Enter any username and password you want to bypass";


        }

        protected void LoginClick(object sender, EventArgs e) {
            using (Authenticate auth = new Authenticate())
            {
                byte[] passwordbytes = auth.Encode(UserPass.Text);
                System.Diagnostics.Debug.WriteLine(auth.Hash(passwordbytes));
                Msg.Text = auth.Hash(passwordbytes);

                // Privileged Execution
                FormsAuthentication.RedirectFromLoginPage("Prototype User", false);

            }
        }
	}
}