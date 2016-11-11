using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet {
    public partial class SiteMaster : MasterPage {
        protected void Page_Load(object sender, EventArgs e) {
            if (Authenticate.IsAuthenticated())
            {
                LeftBtn.InnerText = "Email: " + Authenticate.GetEmail();
                LeftBtn.HRef = "#";

                RightBtn.InnerText = "Sign Out";
                RightBtn.HRef = "~/Auth/SignOut.aspx";
            }
            else
            {
                LeftBtn.InnerText = "Sign In";
                LeftBtn.HRef = "~/Auth/SignIn.aspx";


                RightBtn.InnerText = "Register";
                RightBtn.HRef = "~/Auth/SignUp.aspx";
            }
        }
    }
}