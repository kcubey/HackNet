using HackNet.Security;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;

namespace HackNet.Data
{
    public partial class InventoryItem
    {
		[Key, Column(Order = 1)]
		public int UserId { get; set; }
		[Key, Column(Order = 2)]
		public int ItemId { get; set; }
		public int Quantity { get; set; }

		// Foreign key references
		public virtual Users User { get; set; }


        internal static List<Items> GetUserInvItems(Users user,int itemType)
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

                    itmList.RemoveAll(element => element.ItemType != (ItemType)itemType);
                   
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