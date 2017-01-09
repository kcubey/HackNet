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
            DataContext ctx = new DataContext();
            packageNameLbl.Text = "Package " + Session["packageId"].ToString();
            packagePriceLbl.Text = "$" + Session["packageprice"].ToString();
        }

        public void CancelClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/game/market");
        }

        protected void AuthClick(object sender, EventArgs e)
        {
            string email = Email.Text.ToLower();
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
                Response.Redirect("~/payment/payment");
            }
        }

        

    }
}
