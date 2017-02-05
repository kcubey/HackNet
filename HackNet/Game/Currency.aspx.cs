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
                Users u = CurrentUser.Entity(false, db);
                dbBuck = u.ByteDollars;
                dbCoin = u.Coins;
            }
            buckValidator.MaximumValue = dbBuck.ToString();

            LoadPackages(packageRepeater);

        }

        private void LoadPackages(Repeater rpt)
        {
            List<Pack> pList = Pack.GetPackageList();
            if (pList.Count != 0)
            {
                string imageurlstring;
                string url;
                DataTable dt = new DataTable();
                dt.Columns.Add("PackageId", typeof(int));
                dt.Columns.Add("Quantity", typeof(int));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Price", typeof(double));
                dt.Columns.Add("ItemPic", typeof(string));
                foreach (Pack p in pList)
                {
                    PackItem packItem = PackItem.GetPackageItems(p.PackageId);
                    Items i = HackNet.Data.Items.GetItem(packItem.ItemId);

                    imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                    url = "data:image/png;base64," + imageurlstring;
                    dt.Rows.Add(p.PackageId, packItem.Quantity, p.Description, p.Price, url);
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

        #region Payment stuff
        public void buckTextBox_TextChanged(Object sender, EventArgs e)
        {
            Calculate();
        }

        public void PrintMessage(String message)
        {
            string alert = message;
            Response.Write("<script type='text/javascript'>alert('" + alert + "');document.location.href='currency.aspx';</script>");
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
            catch
            {
                ClearText();
            }
        }

        public void buyPackage_Click(Object sender, EventArgs e)
        {
         //   int packageId = Convert.ToInt32(packageNo.Text);
           // int packagePrice = Convert.ToInt32(packageCost.Text);
            //int itemQuantity = Convert.ToInt32(packageQuantity.Text);
            //KTODO change to retreive package id & price from button/session
            //KTODO: Change currency/payment/reauth/checkout
         //   Session["packageId"] = packageId;
        //    Session["packagePrice"] = packagePrice;
        //    Session["itemQuantity"] = itemQuantity;
            Response.Redirect("~/payment/Reauth", true);
        }

        public void buyPackage_Command(Object sender, CommandEventArgs e)
        {
            int packageId = int.Parse(e.CommandArgument.ToString());

            Pack pkg = Data.Pack.GetPackage(packageId);
            PackItem pkgItems = Data.PackItem.GetPackageItems(packageId);
            Session["pkg"] = pkg;
            Session["pkgItems"] = pkgItems;
            //KTODO change to retreive package id & price from button

            Session["packageId"] = packageId;
    //        Session["packagePrice"] = packagePrice;
            Response.Redirect("~/payment/Reauth", true);
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
                Users u = CurrentUser.Entity(false, db);
                u.ByteDollars = newBuck;
                u.Coins = newCoin;

                db.SaveChanges();
            }

            PrintMessage(message);

        }

        public void modalButton_Click(Object sender, EventArgs e)
        {
            numBuck = Convert.ToInt32(Session["numBuck"]);
            numCoin = Convert.ToInt32(Session["numCoin"]);

            messageLabel.Text = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popupConfirmation", "showPopup();", true);

        }

        public void mcButton_Click(Object sender, EventArgs e)
        {
            int newBuck = dbBuck - numBuck;
            int newCoin = dbCoin + numCoin;

            using (DataContext db = new DataContext())
            {
                Users u = CurrentUser.Entity(false, db);
                u.ByteDollars = newBuck;
                u.Coins = newCoin;

                db.SaveChanges();
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
        
                

             

        
        protected void ViewMore_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            Items item = Data.Items.GetItem(id);
            Session["Item"] = item;
            Server.Transfer("PartsInfo.aspx", true);
        }
    }
}