using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace HackNet.Market
{
    public partial class Market : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            buckTextBox.Text = "";
            coinTextBox.Text = "";

        }

        public void getCoinsBucksDB(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Users u = Authenticate.GetCurrentUser();
                int pCoin = u.Coins;
                int pBuck = u.ByteDollars;
            }

        }

        public void buckTextBox_TextChanged(object sender, EventArgs e)
        {
            int numCoin = Convert.ToInt32(buckTextBox.Text) * 100;
            coinTextBox.Text = Convert.ToString(numCoin);
        }

        public void ConversionButton_Click(Object sender, EventArgs e)
        {
            string numBuck = buckTextBox.Text;
            string numCoin = coinTextBox.Text;
            string message = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";

            Response.Write("<script type='text/javascript'>alert('"+ message +"');</script>");

            //    Response.Write("<script type='text/javascript'>window.open('Page.aspx?ID=" + YourTextField.Text.ToString() + "','_blank');</script>");

            //insert add numCoin db code
            //insert minus numBuck db code

        }

        /*
        protected void ConversionButton_Convert(object sender, EventArgs e)
        {
            if ((Information.IsNumeric(buckTextBox.Text) == false))
            {
                lblError.Visible = true;
            }
            else if ((string.IsNullOrEmpty(buckTextBox.Text)))
            {
                lblError.Visible = true;
            }
            else
            {
                lblError.Visible = false;
                buckTextBox.Text = buckTextBox.Text * 0.72387;
            }
        }
        */


        /*private void buckTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            else
            {
                lblError.Visible = true;
            }
        }
        */


        /*
                protected void Pay_Redirect_Click(Object sender, EventArgs e)
                {
                    Response.Redirect("/game/payment/Checkout.aspx", false);
                    /*
                     * or Server.Transfer("Checkout.aspx",true);
                     * reference stackoverflow.com/questions/224569
                     * reference forums.asp.net/t/1331559 for storing information in cookies

                }
            */
    }
}