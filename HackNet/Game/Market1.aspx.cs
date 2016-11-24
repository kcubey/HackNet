using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Market
{
    public partial class Market1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
        }

        #region Navigation Links
        protected void LnkPts(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Parts");
        }

        protected void LnkCurr(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Currency");
        }
        #endregion
    }
}