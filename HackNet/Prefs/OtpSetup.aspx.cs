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
			ExistingOTP.Visible = true;
			if(ExistingOTP.Visible == true)
			{
				OTPSubmitBtn.Text = "Reconfigure OTP";
			}
		}

		protected void VerifyOTP_Button(object sender, EventArgs e)
		{ 
		}

		protected void DisableTOTP_Button(object sender, EventArgs e)
		{
		}

		protected bool OTPCheck(int otpentered, OTPTool ot)
		{
			return true;
		}

	}
}