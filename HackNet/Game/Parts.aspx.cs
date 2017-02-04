using HackNet.Data;
using HackNet.Game;
using HackNet.Game.Class;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Parts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext())
            {
                
                List<Items> ilist = Data.Items.GetItems(-1);
                List<Items> userList = ItemLogic.GetUserInvItems(CurrentUser.Entity(),-1,db);

                ItemLogic.LoadInventory(PartsList, ilist, -1);
                ItemLogic.LoadInventory(ProcessList, ilist, 1);
                ItemLogic.LoadInventory(graphicslist, ilist, 4);
                ItemLogic.LoadInventory(memorylist, ilist, 2);
                ItemLogic.LoadInventory(powersuplist, ilist, 3);
                ItemLogic.LoadInventory(boosterlist, ilist, 0);
                ItemLogic.LoadInventory(UserInvList, userList, -1);
            }
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


        protected void btnAddListing_Click(object sender, EventArgs e)
        {
            MarketListings mklist = new MarketListings();

            using (DataContext db = new DataContext())
            {
                db.MarketListings.Add(mklist);
                db.SaveChanges();
            }
        }


        protected void ViewMore_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            Items item = Data.Items.GetItem(id);
            Session["Item"] = item;
            Response.Redirect("PartsInfo.aspx",true);
        }

        protected void btnSubmitDdl_Click(object sender, EventArgs e)
        {

            //Retrieve Selected Text from Dropdown
            lblSelectedText.Text = ddlParts.SelectedItem.Text;

            //Retrieve Selected Value from Dropdown
            lblSelectedValue.Text = ddlParts.SelectedValue;
        }

        protected void SellItem_Command(object sender, CommandEventArgs e)
        {
            ConfirmSellItemName.Text = ItemName.Text;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SellItemModal", "showSellItemModal()", true);
        }

        protected void SellItemBtn_Click(object sender, CommandEventArgs e)
        {

        }
    }
}
