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
                List<Items> userList = ItemLogic.GetUserInvItems(CurrentUser.Entity(), -1, db);

                ItemLogic.LoadInventory(PartsList, ilist, -1);
                ItemLogic.LoadInventory(ProcessList, ilist, 1);
                ItemLogic.LoadInventory(graphicslist, ilist, 4);
                ItemLogic.LoadInventory(memorylist, ilist, 2);
                ItemLogic.LoadInventory(powersuplist, ilist, 3);
                ItemLogic.LoadInventory(boosterlist, ilist, 0);
                ItemLogic.LoadInventory(UserInvList, userList, -1);
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
            Response.Redirect("PartsInfo.aspx", true);
        }

        protected void SellItem_Command(object sender, CommandEventArgs e)
        {

            Items i = Data.Items.GetItem(int.Parse(e.CommandArgument.ToString()));
            Cache["ItemIDToSell"] = i.ItemId;
            int pricetosell = i.ItemPrice / 2;
            Cache["PriceToSell"] = pricetosell;
            ConfirmSellItemName.Text = i.ItemName;
            ConfirmSellItemPrice.Text = pricetosell.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SellItemModal", "showSellItemModal()", true);
        }

        protected void CfmSellBtn_Click(object sender, EventArgs e)
        {
            if (Cache["ItemIDToSell"] is int)
            {
                if (ItemLogic.DeleteItemFromUserInv(CurrentUser.Entity().UserID, (int)Cache["ItemIDToSell"]))
                {
                    using (DataContext db = new DataContext())
                    {
                        Users u = CurrentUser.Entity(false, db);
                        u.Coins = u.Coins+int.Parse(Cache["PriceToSell"].ToString());
                        db.SaveChanges();
                    }
                    Response.Redirect("Parts.aspx", true);
                }
                else
                {
                    // need to pop up something
                }
            }
        }
    }
}
