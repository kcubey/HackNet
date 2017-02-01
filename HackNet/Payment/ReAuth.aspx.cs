using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;

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
                packagePriceLbl.Text = "$" + Session["packageprice"].ToString();
            }
            catch
            {
                Response.Redirect("~/game/market", true);
            }
            
        }

        public void CancelClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/game/currency", true);
            Session["packageId"] = null;
            Session["packageprice"] = null;
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
            using (Authenticate auth = new Authenticate(email))
            {
                AuthResult result = auth.ValidateLogin(UserPass.Text);
                switch (result)
                {
                    case (AuthResult.Success):
                        AuthSuccess(email);
                        break;
                    case (AuthResult.PasswordIncorrect):
                        Msg.Text = "User and/or password not found (1)";
                        break;
                    case (AuthResult.UserNotFound):
                        Msg.Text = "User and/or password not found (2)";
                        break;
                    default:
                        Msg.Text = "Unhandled error has occured";
                        break;
                }
            }
        }

        private void AuthSuccess(string email)
        {
            using (Authenticate a = new Authenticate(email))
            {
                Response.Cookies.Add(a.AuthCookie);
                Response.Redirect("~/payment/payment", true);
            }
        }

        

    }
}
