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
            string packageName = "A";

            Users u = Authenticate.GetCurrentUser();
            using (MailClient mc = new MailClient(u.Email))
            {
                mc.Subject = "Purchase from HackNet";
            	mc.AddLine("Thank you for buying Package "+packageName +"!");
            	mc.AddLine("We hope you enjoy your gaming experience with us.");
                mc.AddLine("");
            	mc.AddLine("If you did not conduct this purchase, please contact our Support staff at support@haxnet.azurewebsites.net.");
                mc.AddLine("");
                mc.AddLine("Thank you.");
            	mc.Send(u.FullName);
            }
        }
    }
}