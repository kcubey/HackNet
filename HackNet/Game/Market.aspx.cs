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

            Users u = Authenticate.GetCurrentUser();
            int pBuck = 30;
                //u.ByteDollars;

            buckValidator.MaximumValue = pBuck.ToString();
        }

        
        public void PrintMessage(String message)
        {
            string alert = message;
            Response.Write("<script type='text/javascript'>alert('" + alert + "');</script>");

            Debug.WriteLine("exiting printmessage");

        }

        public void CheckValueButton_Click(Object sender, EventArgs e)
        {
            int numBuck = Convert.ToInt32(buckTextBox.Text);
            coinLabel.Text = (numBuck * 100).ToString();

            //Calculate();
        }

        public string Calculate()
        {
            int numBuck = Convert.ToInt32(buckTextBox.Text);
            coinTextBox.Text = (numBuck * 100).ToString();
            return coinTextBox.Text;
        }
        
        public void RedirectButton_Click(Object sender, EventArgs e)
        {
            Response.Redirect("~/payment/Reauth", false);
        }

        public void ConversionButton_Click(Object sender, EventArgs e)
        {
            string message = "";
            
                string numBuck = buckTextBox.Text;
                string numCoin = Calculate(); //check this function
                Debug.WriteLine("check value buck" + numBuck);
                Debug.WriteLine("check value coin" + numCoin);

                message = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";
            
            PrintMessage(message);

            Debug.WriteLine("exiting conversion event");

            //Response.Write("<script type='text/javascript'>alert('"+ message +"');</script>");

            //    Response.Write("<script type='text/javascript'>window.open('Page.aspx?ID=" + YourTextField.Text.ToString() + "','_blank');</script>");

            //insert add numCoin db code
            //insert minus numBuck db code

        }

       


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