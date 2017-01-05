using HackNet.Data;
using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using HackNet.Security;

namespace HackNet.Game
{
    public partial class Inventory : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadInventory(AllPartList,-1);
            LoadInventory(ProcessList,1);
            LoadInventory(GPUList ,4);
        }

        private void LoadInventory(DataList dl,int itemType)
        {
            List<InventoryItem> invList = InventoryItem.GetUserInvList(Authenticate.GetCurrentUser());
            List<Items> ilist = InventoryItem.GetUserInvItems(invList,itemType);
            if (ilist[0]!=null)
            {
                string imageurlstring;
                string url;
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("ItemPic", typeof(string));
                foreach (Items i in ilist)
                {
                    imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                    url = "data:image/png;base64," + imageurlstring;
                    dt.Rows.Add(i.ItemName, url);
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

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            Items item = new Items();
            item.ItemName = ItemName.Text;
            item.ItemType = (ItemType)Int32.Parse(ItemTypeList.SelectedItem.Value);

            Stream strm = UploadPhoto.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(strm);
            item.ItemPic= br.ReadBytes((int)strm.Length);
            item.ItemDesc = ItemDesc.Text;
            item.ItemPrice = Int32.Parse(ItemPrice.Text);
            item.ItemBonus = Int32.Parse(ItemStat.Text);
            using(DataContext db=new DataContext())
            {
                db.Items.Add(item);
                db.SaveChanges();
            }
        }

        protected void AddItemIntoUserBtn_Click(object sender, EventArgs e)
        {
            InventoryItem invitem = new InventoryItem();
            invitem.UserId = int.Parse(UserIDLbl.Text);
            invitem.ItemId = int.Parse(ItemIDLbl.Text);
            invitem.Quantity = int.Parse(QuanLbl.Text);
            using (DataContext db = new DataContext())
            {
                db.InventoryItem.Add(invitem);
                db.SaveChanges();
            }
        }
    }
}