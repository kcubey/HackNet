using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Payment
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            try
            {
                packageDetailsLbl.Text = "Package " + Session["packageId"].ToString() + " - $" + Session["packageprice"].ToString();
            }
            catch
            {
                Response.Redirect("~/game/currency");
            }
            */
            if (Session["packageId"] == null)
            {
                Response.Redirect("~/game/market1");
            }

            string packageName = Session["packageId"].ToString();
            string packagePrice = Session["packageprice"].ToString();
            string transactionDetails = Session["transactionId"].ToString();

            transactionId.Text = transactionDetails;
            string message = "Package " + packageName + " at $" + packagePrice;
            packageDetailsLbl.Text = message;

            Session["packageId"] = null;
            Session["packageprice"] = null;


            Users u = CurrentUser.Entity();
            using (MailClient mc = new MailClient(u.Email))
            {
                mc.Subject = "Purchase from HackNet";
            	mc.AddLine("Thank you for buying " +message + "!");
                mc.AddLine("Your Transaction Id is "+transactionDetails);
            	mc.AddLine("We hope you enjoy your gaming experience with us.");
                mc.AddLine("");
            	mc.AddLine("If you did not conduct this purchase, please contact our Support staff at support@haxnet.azurewebsites.net, quoting your transaction ID.");
                mc.AddLine("");
                mc.AddLine("Thank you.");
            	mc.Send(u.FullName);
            }
        }
    }
}