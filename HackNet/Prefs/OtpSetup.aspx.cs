using HackNet.Data;
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
			{                               // Initial page load only
				using (Authenticate a = new Authenticate())         // Create auth instance with current user's email
				{
					if (a.Is2FAEnabled)                             // Check if 2FA is already enabled
					{
						VerifyForConfigure.Text = "Reconfigure OTP";
						VerifyOTP.Text = "Verify OTP and Reconfigure 2FA";
					}
					else
					{
						VerifyForDisable.Visible = false;
						VerifyForConfigure.Text = "Configure OTP";
						VerifyOTP.Text = "Verify OTP and Enable 2FA";
					}
				}
			}
		}

		protected void VerifyForConfigure_Button(object sender, EventArgs e)
		{
			using (Authenticate a = new Authenticate())
			{
				if (a.ValidateLogin(CurrPWTxt.Text) == AuthResult.Success)
				{
					OTPStep1.Visible = false;
					OTPStep2.Visible = true;
					Base32Lbl.Text = GenerateOtp();
					if (ViewState["GeneratedSecret"] != null && ViewState["GeneratedSecret"] is string)
						OTPUriQrImg.ImageUrl = OTPTool.QrCodeUrl(ViewState["GeneratedSecret"] as string);
				} else
				{
					Msg.Text = "Incorrect password entered, please try again.";
				}
			}
		}

		protected void VerifyForDisable_Button(object sender, EventArgs e)
		{
			using (Authenticate a = new Authenticate())
			{
				if (a.ValidateLogin(CurrPWTxt.Text) == AuthResult.Success)
				{
					OTPStep1.Visible = false;
					OTPDisable.Visible = true;
				}
				else
				{
					Msg.Text = "Incorrect password entered, please try again.";
				}
			}
		}

		protected void VerifyToSetOTP_Button(object sender, EventArgs e)
		{
			string b32sec = ViewState["GeneratedSecret"] as string;
			if (string.IsNullOrEmpty(b32sec))
				return;				
			using (Authenticate a = new Authenticate())
			{
				switch (a.Validate2FA(GeneratedOtpTxt.Text, b32sec))
				{
					case OtpResult.Success:
						a.Set2FASecret(b32sec);
						Msg2.Text = "Successfully set 2FA, redirecting you in 5 seconds";
						Response.AddHeader("REFRESH", "5;URL=/");
						break;
					case OtpResult.NotInt:
						Msg2.Text = "You entered non-numbers, please check again";
						break;
					case OtpResult.WrongLength:
						Msg2.Text = "You entered an OTP with malformed length";
						break;
					case OtpResult.WrongOtp:
						Msg2.Text = "OTP Entered does not match, please try again";
						break;
				}
			}
		}

		protected void DisableOTP_Button(object sender, EventArgs e)
		{
			using (Authenticate a = new Authenticate())
			{
				switch (a.Validate2FA(DisableTotpTxt.Text))
				{
					case OtpResult.Success:
						a.Set2FASecret(null);
						Msg.Text = "Successfully disabled your 2FA, thus your account is less secure.";
						break;
					case OtpResult.NotInt:
						Msg.Text = "You entered non-numbers, please check again";
						break;
					case OtpResult.WrongLength:
						Msg.Text = "You entered an OTP with malformed length";
						break;
					case OtpResult.WrongOtp:
						Msg.Text = "OTP Entered does not match, please try again";
						break;
				}
			}
		}

		private string GenerateOtp()
		{
			string b32secret;
			using (OTPTool otp = new OTPTool())
				b32secret = otp.SecretBase32;
			ViewState["GeneratedSecret"] = b32secret;
			string spacedsecret = b32secret.Substring(0, 4) + "&nbsp";
			spacedsecret += b32secret.Substring(4, 4) + "&nbsp";
			spacedsecret += b32secret.Substring(8, 4) + "&nbsp";
			spacedsecret += b32secret.Substring(12, 4);
			return spacedsecret;
		}

	}
}