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
			Users u = CurrentUser.Entity();
			LoggedInUsername.Text = u.UserName;
			LoggedInName.Text = u.FullName;
			RegisteredDate.Text = u.Registered.ToLongDateString();
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
				if (Authenticate.PasswordStrong(NewUserPass.Text))
				{
					switch (a.UpdatePassword(OldUserPass.Text, NewUserPass.Text))
					{
						case (AuthResult.Success):
							Msg.ForeColor = System.Drawing.Color.LimeGreen;
							Msg.Text = "Password successfully changed";
							return;
						case (AuthResult.PasswordIncorrect):
							Msg.Text = "Old password did not match";
							return;
						case (AuthResult.UserNotFound):
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