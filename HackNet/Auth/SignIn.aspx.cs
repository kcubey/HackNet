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

			string newsalt = Authenticate.Decode64(Authenticate.Generate(64));
			Msg.Text = newsalt;
            DataContext ctx = new DataContext();
            ctx.Users.Find(1).Email = "wtfgoogle@gmail.com";
        }

        protected void LoginClick(object sender, EventArgs e)
        {
			byte[] passwordbytes = Authenticate.Encode64(UserPass.Text);
			using (Authenticate auth = new Authenticate())
            {
				System.Diagnostics.Debug.WriteLine(auth.Hash(passwordbytes));
				Msg.Text = auth.Hash(passwordbytes);

                // Privileged Execution
                FormsAuthentication.RedirectFromLoginPage("Prototyper", false);

            }
        }
	}
}