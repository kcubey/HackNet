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
            if (CurrentUser.IsAuthenticated())
            {
				PrivateLeft.Visible = true;
				PrivateRight.Visible = true;

                PrivateLeftLink.Text = "Hello, " + CurrentUser.GetUserName();

				AccessLevel al = CurrentUser.GetAccessLevel();
				if (al == AccessLevel.Admin || al == AccessLevel.Staff)
				{
					ap.Visible = true;
				}
            }
            else
            {
				PublicLeft.Visible = true;
				PublicRight.Visible = true;
            }
        }
    }
}