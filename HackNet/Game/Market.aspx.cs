using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            lblError.Visible = false;
        }

        public void validateBuck()
        {
            Users u = Authenticate.GetCurrentUser();
            int pBuck = u.ByteDollars;

            try
            {
                int numBuck = int.Parse(buckTextBox.Text);
                if (numBuck < pBuck || numBuck > pBuck)
                {
                    lblError.Visible = true;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
      
        public void PrintMessage(String message)
        {
            string alert = message;
            Response.Write("<script type='text/javascript'>alert('" + alert + "');</script>");

        }

        public void buckTextBox_TextChanged(object sender, EventArgs e)
        {
            string message = "";
            validateBuck();

            if (lblError.Visible == false)
            {
                try
                {
                    int numCoin = int.Parse(buckTextBox.Text) * 100;
                    coinTextBox.Text = Convert.ToString(numCoin);

                }catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }

            }
            else
            {
                message = "Please enter a valid number of bucks to convert.";
                PrintMessage(message);
            }
        }
        
        public void ConversionButton_Click(Object sender, EventArgs e)
        {
            string message = "";

            if (lblError.Visible == true)
            {
                message = "Please enter a valid number of bucks to convert.";
            }
            else if (lblError.Visible == false)
            {
                string numBuck = buckTextBox.Text;
                string numCoin = coinTextBox.Text;
                message = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";
            }
            PrintMessage(message);

            //Response.Write("<script type='text/javascript'>alert('"+ message +"');</script>");

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