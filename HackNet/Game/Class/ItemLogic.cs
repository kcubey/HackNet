using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

namespace HackNet.Game.Class
{
    public class ItemLogic
    {
        // Inventory Logic a
        internal static List<Items> GetUserInvItems(Users user, int itemType)
        {
            try
            {
                using (DataContext db = new DataContext())
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
                    if (itemType !=-1)
                    {
                        itmList.RemoveAll(element => element.ItemType != (ItemType)itemType);
                    }
                    return itmList;
                }

            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");

            }
        }

        // Default Machine Parts
        internal static List<Items> GetDefaultParts()
        {
            List<Items> itmlist;
            using (DataContext db = new DataContext())
            {
                var query = from i in db.Items where i.ItemId == 1 || i.ItemId == 8 || i.ItemId == 9 || i.ItemId == 10 select i;
                itmlist = query.ToList();
            }
            return itmlist;
        }

        // Store Default Parts
        internal static void StoreDefaultParts(List<Items> itmlist)
        {
            InventoryItem inv;
            using(DataContext db=new DataContext())
            {
                foreach(Items i in itmlist)
                {
                    inv = new InventoryItem(Authenticate.GetCurrentUser().UserID,i.ItemId,1);
                    db.InventoryItem.Add(inv);
                    db.SaveChanges();
                }
            }
        }

        // Does Item integrity check
        internal static bool ItemIntegrityCheck(List<Items> itmlist,Items i)
        {


            return false;
        }
    }
}