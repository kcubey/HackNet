using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Prefs
{
	public partial class ProfileSettings : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Users u = Authenticate.GetCurrentUser();
				usernameTxt.Text = u.UserName;
				emailTxt.Text = u.Email;
				fnameTxt.Text = u.FullName;
				DobTxt.Text = u.BirthDate.ToLongDateString();
			}
		}

		protected void ProfileChange_Click(object sender, EventArgs e)
		{
			Page.Validate();
			string whatschanged = "";
			using (Authenticate a = new Authenticate())
			using (DataContext db = new DataContext())
			{
				Users u = Authenticate.GetCurrentUser(false, db);

				if (a.ValidateLogin(CfmPasswordTxt.Text) != AuthResult.Success)
				{
					Msg.Text = "Your entered password was incorrect!";
					return;
				}

				if (!DobTxt.Text.Equals(u.BirthDate.ToLongDateString()))
				{
					DateTime newDob;
					if (DateTime.TryParse(DobTxt.Text, out newDob))
					{
						u.BirthDate = newDob;
						whatschanged += "Birthdate ";
					} else
					{
						Msg.Text = "Error with date of birth input!";
						return;
					}

				}

				// Sequentially check if the details were changed
				if (!usernameTxt.Text.Equals(u.UserName))
				{
					u.UserName = usernameTxt.Text;
					whatschanged += "Username ";
				}

				if (!emailTxt.Text.Equals(u.Email))
				{
					u.Email = emailTxt.Text;
					whatschanged += "Email ";
				}

				if (!fnameTxt.Text.Equals(u.FullName))
				{
					u.FullName = fnameTxt.Text;
					whatschanged += "Name ";

				}

				if (!whatschanged.Equals(""))
				{
					db.SaveChanges();
					Msg.ForeColor = System.Drawing.Color.GreenYellow;
					Msg.Text = "Changes saved successfully!";
				} else
				{
					Msg.Text = "No changes were made.";
				}


			}
		}
	}
}