using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Payment
{
    public partial class Retry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["packageId"] == null || Session["packageprice"] == null)
                {
                    Response.Redirect("~/game/market", true);
                }
            }
            catch
            {
                Response.Redirect("~/game/market", true);
            }
        }
    }
}