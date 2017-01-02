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
			if (Authenticate.IsAuthenticated())
			{
				if (User.IsInRole("Admin"))
					Msg1.Text = "User is in admin role";
				else
					Msg1.Text = "Not in admin role";

				if (User.IsInRole("Staff"))
					Msg2.Text = "User is in staff role";
				else
					Msg2.Text = "Not in staff role";

				if (User.IsInRole("User"))
					Msg3.Text = "User is in user role";
				else
					Msg3.Text = "Not in user role";

				//Users u = Authenticate.GetCurrentUser();
				//using (MailClient mc = new MailClient(u.Email))
				//{
				//	mc.Subject = "Verify your Email Address";
				//	mc.AddLine("Thank you for registering with HackNet!");
				//	mc.AddLine("We hope you enjoy your gaming experience with us,");
				//	mc.AddLine("Kindly verify your email address by clicking on the link below");
				//	mc.Send(u.FullName, "Verify Email", "https://haxnet.azurewebsites.net/");
				//}
			}
		}
	}
}