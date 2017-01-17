using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HackNet.Game.Class
{
    public class ItemLogic
    {
        // Get all Items owned by user
        internal static List<Items> GetUserInvItems(Users user, int itemType, DataContext db)
        {
            var query = from inv in db.InventoryItem where inv.UserId == user.UserID select inv;

            // For debugging Atm
            List<InventoryItem> invlist = query.ToList();
            List<Items> itmList = new List<Items>();
            foreach (InventoryItem inv in invlist)
            {
                for (int i = 0; i < inv.Quantity; i++)
                {
                    itmList.Add(Items.GetItem(inv.ItemId));
                }
            }
            if (itemType != -1)
            {
                itmList.RemoveAll(element => element.ItemType != (ItemType)itemType);
            }
            return itmList;

        }

        // Load Items into Datalist
        internal static void LoadInventory(DataList dl, List<Items> itmlist, int itemType)
        {
            if (itmlist.Count != 0)
            {
                string imageurlstring;
                string url;
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("ItemPic", typeof(string));
                foreach (Items i in itmlist)
                {
                    if (i.ItemType == (ItemType)itemType)
                    {
                        imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                        url = "data:image/png;base64," + imageurlstring;
                        dt.Rows.Add(i.ItemName, url);
                    }
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

        // Default Machine Parts
        internal static List<Items> GetDefaultParts(DataContext db)
        {
            List<Items> itmlist;
            var query = from i in db.Items where i.ItemId == 11 || i.ItemId == 8 || i.ItemId == 9 || i.ItemId == 10 select i;
            itmlist = query.ToList();
            return itmlist;
        }

        // Store Default Parts
        internal static void StoreDefaultParts(DataContext db, int userid)
        {
            InventoryItem inv;
            foreach (Items i in GetDefaultParts(db))
            {
                inv = new InventoryItem(userid, i.ItemId, 1);
                db.InventoryItem.Add(inv);
                db.SaveChanges();
            }
        }

        // Does Item integrity check
        internal static bool ItemIntegrityCheck(List<Items> itmlist, Items i)
        {


            return false;
        }

        // Add item to user inventory
        internal static void AddItemToInventory(Users user, int itemid,int quantity=1)
        {
            using(DataContext db=new DataContext())
            {
                InventoryItem invitem;
                if(CheckInventoryItem(db,user,itemid, out invitem))
                {
                    invitem.Quantity += quantity;
                    db.SaveChanges();
                }else
                {
                    invitem.ItemId = itemid;
                    invitem.UserId = user.UserID;
                    invitem.Quantity = quantity;
                    db.InventoryItem.Add(invitem);
                    db.SaveChanges();
                }
            }
        }

        // Check if item is in inventory
        private static bool CheckInventoryItem(DataContext db, Users user, int itemid, out InventoryItem invItem)
        {

            invItem = (from i in db.InventoryItem where i.UserId == user.UserID && i.ItemId == itemid select i).FirstOrDefault();
            if(invItem == null)
            {
                return false;
                
            }else
            {
                return true;
            }
        }
    }
}