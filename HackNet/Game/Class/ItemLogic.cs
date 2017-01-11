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
        // Inventory Logic
        internal static List<Items> GetUserInvItems(Users user, int itemType)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var query = from inv in db.InventoryItem where inv.UserId == user.UserID select inv;

                    // For debugging Atm
                    List<InventoryItem> invlist = query.ToList();
                    foreach (InventoryItem inv in invlist)
                    {
                        System.Diagnostics.Debug.WriteLine("User Owns: " + inv.ItemId);
                    }

                    List<Items> itmList = new List<Items>();
                    foreach (InventoryItem inv in invlist)
                    {
                        for (int i = 0; i < inv.Quantity; i++)
                        {
                            itmList.Add(Items.GetItem(inv.ItemId));
                            System.Diagnostics.Debug.WriteLine("User Owns: " + Items.GetItem(inv.ItemId).ItemName);
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("Count:  " + invlist.Count);
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
    }
}