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

namespace HackNet.Admin
{
    public partial class PackageMgmt : System.Web.UI.Page
    {
        protected int itemType;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    PackItem packItem = PackItem.GetPackageItems(p.PackageId,true);
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

        protected void btnAddPackage_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Pack pkg = new Pack();
                pkg.Description = pkgDesc.Text;

                string strPrice = pkgPrice.Text;
                pkg.Price = Convert.ToDecimal(strPrice.Replace(" ", ""));

                db.Package.Add(pkg);
                db.SaveChanges();
                Session["pkgId"] = pkg.PackageId;

                PackItem pkgItems = new PackItem();
                pkgItems.PackageId = Convert.ToInt32(Session["pkgId"]);
                pkgItems.ItemId = Convert.ToInt32(Session["itemId"]);
                pkgItems.Quantity = Convert.ToInt32(pkgQuantity.Text);

                db.PackItem.Add(pkgItems);
                db.SaveChanges();
            }

            string alert = "Package Added";
            PrintMessage(alert);
        }

        private DataTable LoadInventory(int itemType)
        {
            List<Items> ilist = Data.Items.GetItems(itemType);

            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemID", typeof(int));
            foreach (Items i in ilist)
            {
                dt.Rows.Add(i.ItemName, i.ItemId);
            }
            return dt;
        }

        protected void DisplayItems(object sender, EventArgs e)
        {
            selectedItemLbl.Text = string.Empty;
            Items item = new Items();
            itemType = Convert.ToInt32(itemTypeDDL.SelectedItem.Value);
            SelectionDataList.DataSource = LoadInventory(itemType);
            SelectionDataList.DataBind();
        }

        protected void SelectedItem_Command(object sender, CommandEventArgs e)
        {
            int itemid = int.Parse(e.CommandArgument.ToString());
            Session["itemId"] = itemid;
            Items i = HackNet.Data.Items.GetItem(itemid);
            selectedItemLbl.Text = i.ItemName.ToString();
        }

        protected void EditPackage_Command(object sender, CommandEventArgs e)
        {
            int packageId = int.Parse(e.CommandArgument.ToString());
            Pack p = HackNet.Data.Pack.GetPackage(packageId,true);
            PackItem pi = HackNet.Data.PackItem.GetPackageItems(packageId, true);
            Items i = HackNet.Data.Items.GetItem(pi.ItemId);

            Session["packageId"] = packageId;
            Session["packageItemId"] = pi.ItemId;
            EditPackageId.Text = p.PackageId.ToString();
            EditItem.Text = i.ItemName.ToString();
            
            //changeable fields:
            EditPackageDesc.Text = p.Description.ToString();
            EditPackagePrice.Text = p.Price.ToString();
            EditItemQuantity.Text = pi.Quantity.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditModal", "showEditModal()", true);
        }

        public void PrintMessage(String message)
        {
            string alert = message;
            Response.Write("<script type='text/javascript'>alert('" + alert + "');document.location.href='packagemgmt.aspx';</script>");
        }

        protected void UpdatePackageBtn_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Pack p = HackNet.Data.Pack.GetPackage(Convert.ToInt32(Session["packageId"]), false,db);
                PackItem pi = HackNet.Data.PackItem.GetPackageItems(Convert.ToInt32(Session["packageId"]), false, db);

                p.Description = EditPackageDesc.Text;
                p.Price = Convert.ToDecimal(EditPackagePrice.Text);
                pi.Quantity = Convert.ToInt32(EditItemQuantity.Text);
                
                db.SaveChanges();
            }
            PrintMessage("Package updated");
        }

        protected void DeletePackageBtn_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                int packid = int.Parse((Session["packageId"].ToString()));
                Pack p = db.Package.Where(x => x.PackageId == packid).FirstOrDefault();
                PackItem pi = db.PackItem.Where(x=>x.PackageId== packid).FirstOrDefault();
                db.PackItem.Remove(pi);
                db.Package.Remove(p);
                db.SaveChanges();
            }
            PrintMessage("Package deleted");
        }

        protected void ViewUserCurr_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserID", typeof(string));
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("User Name", typeof(string));
            dt.Columns.Add("User Buck", typeof(int));
            dt.Columns.Add("User Coin", typeof(int));
            List<Users> allUserList = new List<Users>();
            using (DataContext db = new DataContext())
            {
                allUserList = (from u in db.Users select u).ToList();
            }
            if (allUserList.Count != 0)
            {
                foreach (Users u in allUserList)
                {
                    dt.Rows.Add(u.UserID, u.FullName, u.UserName, u.ByteDollars, u.Coins);
                }
                ViewUserCurr.DataSource = dt;
                ViewUserCurr.DataBind();
            }
            else
            {
                ViewUserCurr.DataSource = null;
                ViewUserCurr.DataBind();
            }
        }

        protected void AddUserCurrency_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(UserIDTxtbox.Text);
            int currType = int.Parse(userCurrencyDDL.SelectedValue);
            int currQty = int.Parse(userQuantityTxtbox.Text);
            using (DataContext db = new DataContext())
            {
                Users u = CurrentUser.Entity(false, db);
                int dbBuck = u.ByteDollars;
                int dbCoin = u.Coins;

                if (currType == 1)
                {
                    int newBuck = dbBuck + currQty;
                    u.ByteDollars = newBuck;
                }
                else if (currType == 2)
                {
                    int newCoin = dbCoin + currQty;
                    u.Coins = newCoin;
                }
                db.SaveChanges();
            }
            PrintMessage("Currency added");
        }

        protected void RemoveUserCurrency_Click(object sender, EventArgs e)
        {
            int userId = int.Parse(UserIDTxtbox.Text);
            int currType = int.Parse(userCurrencyDDL.SelectedValue);
            int currQty = int.Parse(userQuantityTxtbox.Text);
            using (DataContext db = new DataContext())
            {
                Users u = CurrentUser.Entity(false, db);
                int dbBuck = u.ByteDollars;
                int dbCoin = u.Coins;

                if (currType == 1)
                {
                    int newBuck = dbBuck - currQty;
                    u.ByteDollars = newBuck;
                }
                else if (currType == 2)
                {
                    int newCoin = dbCoin - currQty;
                    u.Coins = newCoin;
                }
                db.SaveChanges();
            }
            PrintMessage("Currency removed");
        }
    }
}