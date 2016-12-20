using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Prefs
{
	public partial class OtpSetup : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			using (Authenticate a = new Authenticate())         // Create auth instance with current user's email
			{
				// Check if 2FA is already enabled
				if (a.Is2FAEnabled)
				{
					OTPSubmitBtn.Text = "Reconfigure OTP";
				} else
				{
					OTPSubmitBtn.Text = "Configure OTP";
				}
			}
		}

		protected void VerifyPw_Button(object sender, EventArgs e)
		{
			using (Authenticate a = new Authenticate())
			{
				if (a.ValidateLogin(CurrPWTxt.Text) == Authenticate.AuthResult.Success)
				{
					OTPStep1.Visible = false;
					OTPStep2.Visible = true;
					Base32Lbl.Text = GenerateOtp();
				} else
				{
					Msg.Text = "Incorrect password entered, please try again.";
				}
			}
		}

		protected void VerifyOTP_Button(object sender, EventArgs e)
		{
		}

		protected void DisableTOTP_Button(object sender, EventArgs e)
		{
		}

		private string GenerateOtp()
		{
			using (OTPTool otp = new OTPTool())
				ViewState["OTPSec"] = otp.generateSecret();
			return ViewState["OTPSec"] as string;
		}

		protected bool OTPCheck(int otpentered, OTPTool ot)
		{
			return true;
		}

	}
}