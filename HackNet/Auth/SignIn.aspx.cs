using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HackNet.Security;

namespace HackNet.Auth {
	public partial class SignIn : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
            using (Authenticate auth = new Authenticate())
            {
                byte[] passwordbytes = auth.Encode("halo");
                System.Diagnostics.Debug.WriteLine(auth.Hash(passwordbytes));
                Msg.Text = auth.Hash(passwordbytes);

            }
		}

		protected void LoginClick(object sender, EventArgs e) {
            
		}

		protected void RegisterClick(object sender, EventArgs e) {

		}
	}
}