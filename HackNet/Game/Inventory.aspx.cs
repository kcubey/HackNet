using HackNet.Data;
using System;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using HackNet.Security;
using HackNet.Game.Class;
using System.Web.UI;

namespace HackNet.Game
{
    public partial class Inventory : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataContext db=new DataContext())
            {
                System.Collections.Generic.List<Items> ilist = ItemLogic.GetUserInvItems(CurrentUser.Entity(), -1, db);
                ItemLogic.LoadInventory(AllPartList, ilist, -1);
                ItemLogic.LoadInventory(ProcessList, ilist, 1);
                ItemLogic.LoadInventory(GPUList, ilist, 4);
                ItemLogic.LoadInventory(MemoryList, ilist, 2);
                ItemLogic.LoadInventory(PowerSupList, ilist, 3);
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

            InventoryItem invitem = new InventoryItem(int.Parse(UserIDLbl.Text), int.Parse(ItemIDLbl.Text), int.Parse(QuanLbl.Text));
            using (DataContext db = new DataContext())
            {
                db.InventoryItem.Add(invitem);
                db.SaveChanges();
            }
        }

        protected void ViewItem_Command(object sender, CommandEventArgs e)
        {
            int itemid = int.Parse(e.CommandArgument.ToString());
            Items itm = HackNet.Data.Items.GetItem(itemid);

            ItemNameLbl.Text = itm.ItemName;
            ItemTypeLbl.Text = itm.ItemType.ToString();
            ItemDescLbl.Text = itm.ItemDesc;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ItemViewModel", "showItemPopUp();", true);

        }
    }
}