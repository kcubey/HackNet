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
            DataList1.DataSource = LoadInventory(-1);
            DataList1.DataBind();

            ProcessList.DataSource = LoadInventory(1);
            ProcessList.DataBind();

            GPUList.DataSource = LoadInventory(4);
            GPUList.DataBind();


        }

        private DataTable LoadInventory(int itemType)
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
                dt.Rows.Add(i.ItemName, url,i.ItemId);
            }

            //ProcessList.DataSource = dt;
            //ProcessList.DataBind();
            return dt;
        }


        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            Items item = new Items();
            item.ItemName = ItemName.Text;
            item.ItemType = (ItemType)Int32.Parse(ItemTypeList.SelectedItem.Value);

            Stream strm = UploadPhoto.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(strm);
            item.ItemPic = br.ReadBytes((int)strm.Length);
            item.ItemDesc = ItemDesc.Text;
            item.ItemPrice = Int32.Parse(ItemPrice.Text);
            item.ItemBonus = Int32.Parse(ItemStat.Text);
            using (DataContext db = new DataContext())
            {
                db.Items.Add(item);
                db.SaveChanges();
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (UploadPhoto.HasFile)
            {
                Stream strm = UploadPhoto.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(strm);
                // this is the thing u need to throw into the database
                byte[] imageByte = br.ReadBytes((int)strm.Length);

                string base64string = Convert.ToBase64String(imageByte, 0, imageByte.Length);
                imgViewFile.ImageUrl = "data:image/png;base64," + base64string;

            }
        }

        protected void EditItemBTn_Command(object sender, CommandEventArgs e)
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

        
        protected void UpdatePartsInfoBtn_Click(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                Items i = Data.Items.GetItem(int.Parse(Cache["ItemID"].ToString()),-1,false,db);
                i.ItemName = EditItemName.Text;
                i.ItemDesc = EditItemDesc.Text;
                i.ItemPrice = int.Parse(EditItemPrice.Text);
                i.ItemBonus = int.Parse(EditItemBonus.Text);

                db.SaveChanges();
            }
        }
        
    }
}