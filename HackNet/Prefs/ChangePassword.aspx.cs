using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;

namespace HackNet.Prefs
{
	public partial class ChangePassword : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Users u = Authenticate.GetCurrentUser();
			LoggedInUsername.Text = u.UserName;
			LoggedInName.Text = u.FullName;
			RegisteredDate.Text = u.Registered.ToString();
		}

		protected void ChangePassClick(object sender, EventArgs e)
		{
			if (!NewUserPass.Text.Equals(NewUserPassCfm.Text))
			{
				Msg.Text = "Password confirmation did not match";
				return;
			}
			using (Authenticate a = new Authenticate())
			{
				if (a.PasswordStrong(NewUserPass.Text))
				{
					switch (a.UpdatePassword(Authenticate.GetEmail(), OldUserPass.Text, NewUserPass.Text))
					{
						case (Authenticate.AuthResult.Success):
							Msg.ForeColor = System.Drawing.Color.LimeGreen;
							Msg.Text = "Password successfully changed";
							return;
						case (Authenticate.AuthResult.PasswordIncorrect):
							Msg.Text = "Old password did not match";
							return;
						case (Authenticate.AuthResult.UserNotFound):
							Msg.Text = "User not found! You may have been logged out";
							return;
						default:
							Msg.Text = "Unknown error has occured, please contact an admin";
							break;
					}
				} else
					Msg.Text = "Please use alphanumeric passwords with at least 8 characters";
				return;

			}
		}
	}
}