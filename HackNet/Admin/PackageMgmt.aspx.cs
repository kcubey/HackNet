using HackNet.Data;
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
            Response.Write("<script type='text/javascript'>alert('" + alert + "');document.location.href='PackageMgmt.aspx';</script>");

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
            Pack p = HackNet.Data.Pack.GetPackage(packageId);
            PackItem pi = HackNet.Data.PackItem.GetPackageItems(packageId);
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

        protected void UpdatePackageBtn_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Pack p = HackNet.Data.Pack.GetPackage((int)Session["packageId"]);
                PackItem pi = HackNet.Data.PackItem.GetPackageItems((int)Session["packageId"]);
                Items i = HackNet.Data.Items.GetItem((int)Session["packageItemId"]);

                p.Description = EditPackageDesc.Text;
                p.Price = Convert.ToDecimal(EditPackagePrice.Text);
                pi.Quantity = Convert.ToInt32(EditItemQuantity.Text);
                
                db.SaveChanges();
            }
            LoadPackages(packageRepeater);
        }

        protected void DeletePackageBtn_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Pack p = HackNet.Data.Pack.GetPackage((int)Session["packageId"]);
                PackItem pi = HackNet.Data.PackItem.GetPackageItems((int)Session["packageId"]);
                db.Package.Remove(p);
                db.PackItem.Remove(pi);
                db.SaveChanges();
            }
            LoadPackages(packageRepeater);
        }

    }
}