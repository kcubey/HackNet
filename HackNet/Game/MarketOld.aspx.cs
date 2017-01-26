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


namespace HackNet.Game
{
    public partial class MarketOld : System.Web.UI.Page
    {
        protected int dbBuck;
        protected int dbCoin;
        protected int numBuck;
        protected int numCoin;
        protected string strBuck;
        protected string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            //updating only
            using (DataContext db = new DataContext())
            {
                Users u = CurrentUser.Entity(false, db);
                //u.ByteDollars = 50;
                //u.Coins = 1200;
                //u.ByteDollars;
                //can consider viewdata / viewstate
                dbBuck = u.ByteDollars;
                dbCoin = u.Coins;

            }
            buckValidator.MaximumValue = dbBuck.ToString();
            Debug.WriteLine("user has " + dbBuck + " bucks and " + dbCoin + " coins");
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
                numBuck = Convert.ToInt32(strBuck.Replace(" ", ""));
                if (numBuck < dbBuck && numBuck > 0)
                {
                    numCoin = (numBuck * 100);
                    convertedCoinLabel.Text = numCoin.ToString();
                    Session["numBuck"] = numBuck;
                    Session["numCoin"] = numCoin;
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
            int packageId = Convert.ToInt32(packageNo.Text);
            int packagePrice = Convert.ToInt32(packageCost.Text);
            // change to retreive package id & price from button

            Session["packageId"] = packageId;
            Session["packagePrice"] = packagePrice;
            Response.Redirect("~/payment/Reauth");
        }

        public void ConversionButton_Click(Object sender, EventArgs e)
        {
            numBuck = Convert.ToInt32(Session["numBuck"]);
            numCoin = Convert.ToInt32(Session["numCoin"]);

            message = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";

            int newBuck = dbBuck - numBuck;
            int newCoin = dbCoin + numCoin;

            using (DataContext db = new DataContext())
            {
                Users u = CurrentUser.Entity(false, db);
                u.ByteDollars = newBuck;
                u.Coins = newCoin;

                db.SaveChanges();
                Debug.WriteLine("user now has " + u.ByteDollars + " bucks and " + u.Coins + " coins");
                //dbBuck = u.ByteDollars;
                dbCoin = u.Coins;
            }

            PrintMessage(message);
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