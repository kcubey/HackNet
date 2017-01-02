using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class PartsInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Items item = (Items)Session["Item"];
            ItemName.Text = item.ItemName;
        }
    }
}