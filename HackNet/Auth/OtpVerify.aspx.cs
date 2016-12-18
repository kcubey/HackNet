using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Auth
{
	public partial class OtpVerify : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["PasswordSuccess"] == null)
			{
				Response.Redirect("~/Default");
			}
		}

		protected void ConfirmOTP(object sender, EventArgs e)
		{
			int totp;
			int.TryParse(OTPValue.Text, out totp);
			string email = Session["PasswordSuccess"] as string;
			using (Authenticate a = new Authenticate(email))
			{
				if (a.Validate2FA(totp))
				{
					Session["PasswordSuccess"] = null;
					FormsAuthentication.SetAuthCookie(email, false);
					Response.Redirect("~/Default");
				}
				else
					Msg.Text = "Incorrect OTP entered!";
			}
		}


		protected void CancelOTP(object sender, EventArgs e)
		{
			string email = Session["PasswordSuccess"] as string;
			FormsAuthentication.SetAuthCookie(email, false);
			Response.Redirect("~/Default");
		}
	}
}