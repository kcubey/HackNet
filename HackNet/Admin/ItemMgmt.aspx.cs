using HackNet.Data;
using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Web.UI;

namespace HackNet.Admin
{
    public partial class ItemMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadAdminItem(-1, AllItemList);

        }
        private void LoadAdminItem(int itemType,DataList dl)
        {

            List<Items> ilist = Data.Items.GetItems(itemType);
            string imageurlstring;
            string url;
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemPic", typeof(string));
            dt.Columns.Add("ItemID", typeof(int));
            foreach (Items i in ilist)
            {
                imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                url = "data:image/png;base64," + imageurlstring;
                dt.Rows.Add(i.ItemName, url, i.ItemId);
            }

            dl.DataSource = dt;
            dl.DataBind();
        }

        protected void EditItemBtn_Command(object sender, CommandEventArgs e)
        {
            int itemid = int.Parse(e.CommandArgument.ToString());
            Items i = HackNet.Data.Items.GetItem(itemid);
            Cache["ItemID"] = itemid;
            EditItemName.Text = i.ItemName.ToString();
            EditItemType.Text = i.ItemType.ToString();
            EditItemDesc.Text = i.ItemDesc.ToString();
            EditItemPrice.Text = i.ItemPrice.ToString();
            EditItemBonus.Text = i.ItemBonus.ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditItemModal", "showEditItemModal()", true);

        }


        // Edit of items
        protected void UpdatePartsInfoBtn_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Items i = Data.Items.GetItem(int.Parse(Cache["ItemID"].ToString()), -1, false, db);
                i.ItemName = EditItemName.Text;
                i.ItemDesc = EditItemDesc.Text;
                i.ItemPrice = int.Parse(EditItemPrice.Text);
                i.ItemBonus = int.Parse(EditItemBonus.Text);

                db.SaveChanges();
            }
        }

        protected void ConfirmDeletePartsInfoBtn_Click(object sender, EventArgs e)
        {
            ConfirmDeleteItemName.Text = EditItemName.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteItemModal", "showDeleteItemModal()", true);
        }

        protected void DeletePartsInfoBtn_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Items i = Data.Items.GetItem(int.Parse(Cache["ItemID"].ToString()), -1, false, db);
                InventoryItem inv = db.InventoryItem.Where(x => x.ItemId == i.ItemId).FirstOrDefault();
                if (inv != null)
                {
                    db.InventoryItem.Remove(inv);
                }

                db.Items.Remove(i);
                db.SaveChanges();
            }
        }
    }
}