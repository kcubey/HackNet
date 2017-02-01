using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

using HackNet.Security;
using System.Text;
using HackNet.Data;

namespace HackNet.Auth
{
	public partial class Debug : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (CurrentUser.IsAuthenticated())
			{
				UDetails1.Text = "User ID: " + CurrentUser.GetUserId().ToString();
				UDetails2.Text = "Username: " + CurrentUser.GetUserId();
				UDetails3.Text = "Access Level: " + Enum.GetName(typeof(AccessLevel), CurrentUser.GetAccessLevel());
			}
			Msg.Text = Request.ServerVariables["HTTP_HOST"];
		}

		protected void SendMail()
		{
			Users u = CurrentUser.Entity();
			using (MailClient mc = new MailClient(u.Email))
			{
				mc.Subject = "Verify your Email Address";
				mc.AddLine("Thank you for registering with HackNet!");
				mc.AddLine("We hope you enjoy your gaming experience with us,");
				mc.AddLine("Kindly verify your email address by clicking on the link below");
				mc.Send(u.FullName, "Verify Email", "https://haxnet.azurewebsites.net/");
			}
		}
	}
}