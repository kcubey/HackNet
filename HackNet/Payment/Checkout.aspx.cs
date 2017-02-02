using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Payment
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected string transactionDetails;
        protected string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string packageName = Session["packageId"].ToString();
                string packagePrice = Session["packageprice"].ToString();
                transactionDetails = Session["transactionId"].ToString();
                message = "Package " + packageName + " at $" + packagePrice;
            }
            catch
            {
                Response.Redirect("~/game/currency", true);
            }

            Users u = CurrentUser.Entity();
            MailClient mc = new MailClient(u.Email);
            mc.Subject = "Hacknet Purchase";
            mc.AddLine("Thank you for buying " + message + "!");
            mc.AddLine("Your Transaction Id is " + transactionDetails);
            mc.AddLine("We hope you enjoy your gaming experience with us.");
            mc.AddLine("");
            mc.AddLine("If you did not conduct this purchase, please contact our Support staff at support@haxnet.azurewebsites.net, quoting your transaction ID.");
            mc.AddLine("");
            mc.AddLine("Thank you.");
            mc.Send(u.FullName, "Purchase Made", "https://haxnet.azurewebsites.net/");

            transactionId.Text = transactionDetails;
            packageDetailsLbl.Text = message;
        }
        protected void Page_Unload()
        {
            Session.Abandon();
            Debug.WriteLine("package id: " + Session["packageId"]);
            Debug.WriteLine("package price: " + Session["packageprice"]);
        }

    }
}