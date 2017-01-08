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
        protected int dbBuck;
        protected int numBuck;
        protected int numCoin;
        protected string strBuck;
        protected string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            Users u = Authenticate.GetCurrentUser();
            dbBuck = 30;
                //u.ByteDollars;

            buckValidator.MaximumValue = dbBuck.ToString();
        }

        public void buckTextBox_TextChanged(Object sender, EventArgs e)
        {
            Calculate();
        }

        public void PrintMessage(String message)
        {
            string alert = message;
            Response.Write("<script type='text/javascript'>alert('" + alert + "');</script>");

            ClearText();

        }

        public void Calculate()
        {
            strBuck = buckTextBox.Text;
            try
            {
                numBuck = Convert.ToInt32(strBuck);
                if (numBuck < dbBuck && numBuck > 0)
                {
                    numCoin = (numBuck * 100);
                    convertedCoinLabel.Text = numCoin.ToString();
                }
                else if (numBuck > dbBuck || numBuck < 0)
                {
                    ClearText();
                }
            }
            catch (Exception ex)
            {
                ClearText();
                Debug.WriteLine(ex);
            }
        }
        
        public void buyPackage_Click(Object sender, EventArgs e)
        {
            int packageId = 123;
            int packagePrice = 30;
            // change to retreive package id & price from button

            Session["packageId"] = packageId;
            Session["packagePrice"] = packagePrice;
            Response.Redirect("~/payment/Reauth");
        }

        public void ConversionButton_Click(Object sender, EventArgs e)
        {
            message = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";
            
            PrintMessage(message);


            //insert add numCoin db code
            //insert minus numBuck db code

        }

        public void ClearText()
        {
            buckTextBox.Text = string.Empty;
            convertedCoinLabel.Text = string.Empty;
        }

       
//======================================================================================================

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