using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;

namespace HackNet {
    public partial class Contact : Page {
        protected void Page_Load(object sender, EventArgs e) {

            string message = "this is a message";
            Users u = CurrentUser.Entity();
            MailClient mc = new MailClient(u.Email);
            mc.Subject = "hellosubj";
            mc.AddLine("addline" +message);
            mc.AddLine("This content was User-Generated from ");
            mc.Send("HackNet Team", "go to website", "www.pussy.com");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MailClient mc = new MailClient("keeleyswag@gmail.com");
            mc.Subject = Subjecttxt.Text;
            mc.AddLine(contenttxt.Text);
            mc.AddLine("This content was User-Generated from " + useremail.Text);
            mc.Send("HackNet Team", "go to website", "www.pussy.com");

            Subjecttxt.Text = string.Empty;
            contenttxt.Text = string.Empty;
            useremail.Text = string.Empty;
        }
    }
}