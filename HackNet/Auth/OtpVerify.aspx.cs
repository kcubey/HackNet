using HackNet.Loggers;
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
			SessionValidate();
		}

		protected void ConfirmOTP(object sender, EventArgs e)
		{
			SessionValidate();
					
			string email = Session["PasswordSuccess"] as string;
			if (email == null)
				Response.Redirect("~/Default");

			using (Authenticate a = new Authenticate(email))
			{
				switch (a.Validate2FA(OTPValue.Text))
				{
					case OtpResult.Success:
						Session["PasswordSuccess"] = null;
						LoginSuccess();						
						break;
					case OtpResult.NotInt:
						Msg.Text = "You entered non-numbers, please check again";
						break;
					case OtpResult.WrongLength:
						Msg.Text = "You entered an OTP with malformed length";
						break;
					case OtpResult.WrongOtp:
						Msg.Text = "OTP Entered does not match, please try again";
						AuthLogger.Instance.TOTPFail(a.Email, a.UserId);
						break;
				}
			}
		}



		protected void CancelOTP(object sender, EventArgs e)
		{
			LoginSuccess();
		}

		private void LoginSuccess() {

			SessionValidate();

			string returnurl = Session["ReturnUrl"] as string;
			HttpCookie cookie = Session["Cookie"] as HttpCookie;
			string email = Session["LoginSuccess"] as string;
			int userid = (int) Session["UserId"];

			AuthLogger.Instance.TOTPSuccess(email, userid);
			Response.Cookies.Add(cookie);

			// Assign the keystore to make it valid
			Session["KeyStore"] = Session["TempKeyStore"];

			// Null all unused properties
			Session["ReturnUrl"] = null;
			Session["LoginSuccess"] = null;
			Session["Cookie"] = null;

			if (returnurl != null)
				Response.Redirect(returnurl);
			else
				Response.Redirect("~/Default");
		}

		/// <summary>
		/// Ensure that session contains the required information
		/// </summary>
		private void SessionValidate()
		{
			if (!(Session["PasswordSuccess"] is string))
			{
				System.Diagnostics.Debug.WriteLine("Session: PwdSuccess (email) is invalid or empty");
				Response.Redirect("~/Default", true);
			}

			if (!(Session["Cookie"] is HttpCookie))
			{
				System.Diagnostics.Debug.WriteLine("Session: Cookie is invalid or empty");
				Response.Redirect("~/Default", true);
			}

			if (!(Session["TempKeyStore"] is KeyStore))
			{
				System.Diagnostics.Debug.WriteLine("Session: KeyStore is invalid or empty");
				Response.Redirect("~/Default", true);
			}

			if (!(Session["UserId"] is int))
			{
				System.Diagnostics.Debug.WriteLine("Session: UserId is invalid or empty");
				Response.Redirect("~/Default", true);
			}
		}
	}
}