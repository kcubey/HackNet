using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;

namespace HackNet {
    public partial class About : Page {
        protected void Page_Load(object sender, EventArgs e) {


        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MailClient mc = new MailClient("hacknet@wlgoh.com");
            mc.Subject = Subjecttxt.Text;
            mc.AddLine(contenttxt.Text);
            mc.AddLine("This content was User-Generated from " + useremail.Text);
            mc.Send("HackNet Team", "go to website", "www.pussy.com");
        }
    }
}