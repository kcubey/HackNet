using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Auth
{
	public partial class ResetPassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string email = Request.QueryString["Email"];
			string cfmcode = Request.QueryString["Code"];

			if (email != null && cfmcode != null)
			{
				ResetTable.Visible = false;
				ResetPasswordBtn.Visible = false;
				Confirm(email, cfmcode);
			}
		}

		protected void SendResetLink_Click(object sender, EventArgs e)
		{
			string email = Email.Text;
			Email.Text = "";
			EmailConfirm.SendEmailForPasswordReset(email);

			ResetTable.Visible = false;
			ResetPasswordBtn.Visible = false;
			Msg.Text = "Email has been sent, please check your inbox!";
		}

		protected void Confirm(string email, string cfmcode)
		{
			switch (EmailConfirm.ValidatePasswordReset(email, cfmcode))
			{
				case EmailConfirmResult.Failed:
					Msg.Text = "Something went wrong with the link, please try again";
					break;
				case EmailConfirmResult.Success:
					Msg.Text = "Check your email for your new password";
					Msg.ForeColor = System.Drawing.Color.GreenYellow;
					break;
				case EmailConfirmResult.UserNotFound:
				case EmailConfirmResult.OtherError:
					Msg.Text = "User not found or an error has occured";
					break;
			}
		}
	}
}