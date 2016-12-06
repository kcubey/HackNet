using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet
{
    public partial class GameMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			PlayerName.Text = Authenticate.GetUser().UserName; 
        }

        #region Navigation Links
        protected void LnkHome(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Home");
        }

        protected void LnkWsn(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Workstation");
        }

        protected void LnkMis(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Missions");
        }

        protected void LnkMLog(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Home");
        }

        protected void LnkInv(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Inventory");
        }

		protected void LnkChat(object sender, EventArgs e)
		{
			Response.Redirect("~/Game/Chat");
		}

        protected void LnkMkt(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Market");
        }

        protected void LnkMkt1(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Market1");
        }
        #endregion

    }
}