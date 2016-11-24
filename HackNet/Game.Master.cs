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

        }

        #region Navigation Links
        protected void LnkAnn(object sender, EventArgs e)
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

        protected void LnkEvt(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Home");
        }

        protected void LnkMLog(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Home");
        }

        protected void LnkInv(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Home");
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