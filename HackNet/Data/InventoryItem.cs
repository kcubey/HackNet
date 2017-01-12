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


        public InventoryItem()
        {

        }
        public InventoryItem(int userid,int itemid,int quantity)
        {
            this.UserId = userid;
            this.ItemId = itemid;
            this.Quantity = quantity;
        }

        internal static List<Items> GetUserInvItems(Users user,int itemType)
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