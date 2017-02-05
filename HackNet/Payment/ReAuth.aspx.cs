using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;
using System.Diagnostics;
using HackNet.Loggers;
using System.Text;

namespace HackNet.Payment
{
    public partial class ReAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataContext ctx = new DataContext();
                packageNameLbl.Text = "Package " + Session["packageId"].ToString();
                packagePriceLbl.Text = "$" + Session["packagePrice"].ToString();
            }
            catch
            {
                Response.Redirect("~/game/currency", true);
            }
        }

        public void CancelClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/game/currency", true);
            Session["packageId"] = null;
            Session["packagePrice"] = null;
        }

        protected void AuthClick(object sender, EventArgs e)
        {
            Users u = CurrentUser.Entity();
            string currentEmail = u.Email;

            string email = Email.Text.ToLower();

            if (email != currentEmail)
            {
                Response.Redirect("~/game/currency", true);
            }

            int result = Validate(UserPass.Text, email);

            if (result == 1)
            {
                Response.Redirect("~/payment/payment", true);
            }

            else if (result == 2)
            {
                Msg.Text = "User and/or password not found (1)";
            }
            else if (result == 0)
            {
                Msg.Text = "User and/or password not found (2)";
            }
            else
            {
                Msg.Text = "Unhandled error has occured";
            }
        }
        protected int Validate(string password, string email)
        {
            using (DataContext db = new DataContext())
            {
                Users user = Users.FindByEmail(email, db);
                if (user == null)
                {
                    return 0;
                }
                byte[] bPassword = Encoding.UTF8.GetBytes(password);
                byte[] bSalt = user.Salt;
                byte[] bHash = Crypt.Instance.Hash(bPassword, bSalt);

                if (user.Hash.SequenceEqual(bHash))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
    }
}
