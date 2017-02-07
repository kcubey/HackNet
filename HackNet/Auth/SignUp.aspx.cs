using HackNet.Security;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Auth
{
	public partial class SignUp : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (CurrentUser.IsAuthenticated())
			{
				Response.Redirect("~/Default", true);
			}
		}

		protected void RegisterClick(object sender, EventArgs e)
		{
			// Getting the captcha response for validation
			string captchaResponse = Request.Form["g-Recaptcha-Response"];

			// Guard clauses

			if (!Captcha.Validate(captchaResponse))
			{
				Msg.Text = "Please ensure that the CAPTCHA is done correctly";
				return;
			}

			if (!UserPassCfm.Text.Equals(UserPass.Text))
			{
				Msg.Text = "Passwords do not match";
				return;
			}

			if (!UserEmail.Text.Equals(UserEmail.Text))
			{
				Msg.Text = "Email addresses do not match";
				return;
			}

			// Calling the controller class
			switch (Authenticate.CreateUser(UserEmail.Text, UserName.Text, FullName.Text, UserPass.Text, DateTime.Now))
			{
				case RegisterResult.Success:
					Msg.ForeColor = System.Drawing.Color.LimeGreen;
					RegistrationTable.Visible = false;
                    RegisterButton.Visible = false;
					Msg.Text = "Registration successful, please check your email to confirm it";
					break;
				case RegisterResult.EmailTaken:
					Msg.Text = "Email has already been registered with another user";
					break;
				case RegisterResult.UsernameTaken:
					Msg.Text = "Username has already been taken by another user";
					break;
				case RegisterResult.ValidationException:
					Msg.Text = "Please recheck your input";
					break;
				case RegisterResult.OtherException:
					Msg.Text = "An unexpected error has occured";
					break;
				default:
					Msg.Text = "Nothing interesting happened";
					break;
			}
		}
	}
}