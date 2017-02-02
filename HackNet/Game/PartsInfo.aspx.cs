using HackNet.Data;
using HackNet.Game.Class;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class PartsInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                Items item = Session["Item"] as Items;
                if (item == null)
                {
                    Response.Redirect("Parts.aspx", true);
                }
                else
                {
                    ItemName.Text = item.ItemName;
                    ItemTypeLbl.Text = FindItemType((int)item.ItemType);
                    ItemPrice.Text = item.ItemPrice.ToString();
                    ItemDesc.Text = item.ItemDesc.ToString();
                    string imageurlstring = Convert.ToBase64String(item.ItemPic, 0, item.ItemPic.Length);
                    ItemImageLoaded.ImageUrl = "data:image/png;base64," + imageurlstring;
                }
            }
            catch (Exception ggez)
            {
                Console.WriteLine("Error : " + ggez);
                throw new Exception("Error Code : " + ggez);
            }
        }

        private string FindItemType(int itemtype)
        {
            if (itemtype == 1)
            {
                return "Processor";
            }
            else if (itemtype == 2)
            {
                return "Random Access Memory";
            }
            else if (itemtype == 3)
            {
                return "Power Supply";
            }
            else if (itemtype == 4)
            {
                return "Graphics Processing Unit";
            }
            else if (itemtype == 0)
            {
                return "Booster";
            }
            else
            {
                return "";
            }

        }

        protected void BuyBtn_Click(object sender, EventArgs e)
        {
            Items item = (Items)Session["Item"];
            System.Diagnostics.Debug.WriteLine("Item ID: "+item.ItemId);
            ItemLogic.AddItemToInventory(CurrentUser.Entity(),item.ItemId);

            ItemNameLbl.Text = item.ItemName;
            ItemPriceLbl.Text = item.ItemPrice.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PartTransactionModel", "ShowTransactionBox();", true);

        }

        protected void ExitBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Parts.aspx",true);
        }
    }
}