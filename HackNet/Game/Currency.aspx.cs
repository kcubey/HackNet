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

        protected void LoadPackages(Repeater rpt)
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
                    PackItem packItem = PackItem.GetPackageItems(p.PackageId, true);
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

        protected void buckTextBox_TextChanged(Object sender, EventArgs e)
        {
            Calculate();
        }

        protected void PrintMessage(String message)
        {
            string alert = message;
            Response.Write("<script type='text/javascript'>alert('" + alert + "');document.location.href='currency.aspx';</script>");
        }

        protected void Calculate()
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

        protected void buyPackage_Command(Object sender, CommandEventArgs e)
        {
            int packageId = int.Parse(e.CommandArgument.ToString());

            Pack p = Data.Pack.GetPackage(packageId, true);
            PackItem pi = Data.PackItem.GetPackageItems(packageId, true);
            Items i = Data.Items.GetItem(pi.ItemId);

            Session["packageId"] = p.PackageId;
            Session["packagePrice"] = p.Price;
            Session["itemQuantity"] = pi.Quantity;
            Session["itemId"] = pi.ItemId;
            Session["itemName"] = i.ItemName;

            Response.Redirect("~/payment/Reauth", true);
        }

        protected void ConversionButton_Click(Object sender, EventArgs e)
        {
            numBuck = Convert.ToInt32(Session["numBuck"]);
            numCoin = Convert.ToInt32(Session["numCoin"]);

            int newBuck = dbBuck - numBuck;
            int newCoin = dbCoin + numCoin;

            using (DataContext db = new DataContext())
            {
                Users u = CurrentUser.Entity(false, db);
                u.ByteDollars = newBuck;
                u.Coins = newCoin;

                db.SaveChanges();
            }
            ClearText();
            message = "Conversion complete";
            PrintMessage(message);
            Session.Abandon();
        }

        protected void confirmConvertBtn_Click(Object sender, EventArgs e)
        {
            numBuck = Convert.ToInt32(Session["numBuck"]);
            numCoin = Convert.ToInt32(Session["numCoin"]);

            messageLabel.Text = "Are you sure you want to convert " + numBuck + " buck(s) to " + numCoin + " coins?";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditModal", "showEditModal()", true);
        }

        protected void cancelConvertBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/game/currency", true);
            Session.Abandon();
        }

        protected void ClearText()
        {
            buckTextBox.Text = string.Empty;
            convertedCoinLabel.Text = string.Empty;
        }
    }
}