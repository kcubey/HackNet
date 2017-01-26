using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Currency : System.Web.UI.Page
    {
        protected int dbBuck;
        protected int dbCoin;
        protected int numBuck;
        protected int numCoin;
        protected string strBuck;
        protected string message;

        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Users u = Authenticate.GetCurrentUser(false, db);
                dbBuck = u.ByteDollars;
                dbCoin = u.Coins;
            }
            buckValidator.MaximumValue = dbBuck.ToString();
            Debug.WriteLine("user has " + dbBuck + " bucks and " + dbCoin + " coins");

            LoadInventory(memorylist, 2);
            LoadInventory(packageDL, 5);
        }

        #region Payment stuff
        public void buckTextBox_TextChanged(Object sender, EventArgs e)
        {
            Calculate();
        }

        public void PrintMessage(String message)
        {
            string alert = message;
            Response.Write("<script type='text/javascript'>alert('" + alert + "');</script>");

            ClearText();
            //KTODO: Refresh/reload with updated values
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
            //KTODO change to retreive package id & price from button

            Session["packageId"] = packageId;
            Session["packagePrice"] = packagePrice;
            Response.Redirect("~/payment/Reauth");
        }

        public void ConversionButton_Click(Object sender, EventArgs e)
        {
            numBuck = Convert.ToInt32(Session["numBuck"]);
            numCoin = Convert.ToInt32(Session["numCoin"]);

            message = "Now converting " + numBuck + " buck(s) to " + numCoin + " coins.";

            int newBuck = dbBuck - numBuck;
            int newCoin = dbCoin + numCoin;

            using (DataContext db = new DataContext())
            {
                Users u = Authenticate.GetCurrentUser(false, db);
                u.ByteDollars = newBuck;
                u.Coins = newCoin;

                db.SaveChanges();
                Debug.WriteLine("user now has " + u.ByteDollars + " bucks and " + u.Coins + " coins");
                //dbBuck = u.ByteDollars;
                dbCoin = u.Coins;
            }

            PrintMessage(message);
        }

        public void modalButton_Click(Object sender, EventArgs e)
        {
            numBuck = Convert.ToInt32(Session["numBuck"]);
            numCoin = Convert.ToInt32(Session["numCoin"]);

            messageLabel.Text = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";
        }

        public void mcButton_Click(Object sender, EventArgs e)
        {
            int newBuck = dbBuck - numBuck;
            int newCoin = dbCoin + numCoin;

            using (DataContext db = new DataContext())
            {
                Users u = Authenticate.GetCurrentUser(false, db);
                u.ByteDollars = newBuck;
                u.Coins = newCoin;

                db.SaveChanges();
                Debug.WriteLine("user now has " + u.ByteDollars + " bucks and " + u.Coins + " coins");
                //dbBuck = u.ByteDollars;
                dbCoin = u.Coins;
            }
        }

        public void ClearText()
        {
            buckTextBox.Text = string.Empty;
            convertedCoinLabel.Text = string.Empty;
        }

#endregion

        private void LoadInventory(DataList dl, int itemType) //change to LoadPackages
        {

            List<Items> ilist = Data.Items.GetItems(itemType);
            if (ilist.Count != 0)
            {
                string imageurlstring;
                string url;
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemNo", typeof(int));
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("ItemPic", typeof(string));
                foreach (Items i in ilist)
                {
                    imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                    url = "data:image/png;base64," + imageurlstring;
                    dt.Rows.Add(i.ItemId, i.ItemName, url);
                }

                dl.DataSource = dt;
                dl.DataBind();
            }
            else
            {
                dl.DataSource = null;
                dl.DataBind();
            }
        }
/*
        private void LoadPackages(Repeater rpt, int itemType) //change to LoadPackages
        {

            List<Items> ilist = Data.Items.GetItems(itemType);
            if (ilist.Count != 0)
            {
                string imageurlstring;
                string url;
                DataTable dt = new DataTable();
                dt.Columns.Add("PackageNo", typeof(int));
                dt.Columns.Add("PackagePrice", typeof(string));
                dt.Columns.Add("PackagePic", typeof(string));
                foreach (Items i in ilist)
                {
                    imageurlstring = Convert.ToBase64String(i.PackagePic, 0, i.PackagePic.Length);
                    url = "data:image/png;base64," + imageurlstring;
                    dt.Rows.Add(i.ItemId, i.ItemName, url);
                }

                rpt.DataSource = dt;
                rpt.DataBind();
            }
            else
            {
                rpt.DataSource = null;
                rpt.DataBind();
            }
        }
        */
        //modified from btnAddListing_Click
        protected void btnAddPackage_Click(object sender, EventArgs e)
        {
            Packages pklist = new Packages();

            using (DataContext db = new DataContext())
            {
                db.Packages.Add(pklist);
                db.SaveChanges();
            }
        }

        protected void ViewMore_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            Items item = Data.Items.GetItem(id);
            Session["Item"] = item;
            Server.Transfer("PartsInfo.aspx", true);
        }

        protected void ViewMorePkg_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

      //      Packages pks = Data.Packages.GetDetails(id);
      //      Session["Item"] = pks;
            Server.Transfer("PartsInfo.aspx", true);
        }
    }
}