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

        // May combine this both methods together
        internal static List<InventoryItem> GetUserInvList(Users user)
        {
            int userid = user.UserID;
            try
            {
                using (DataContext db = new DataContext())
                {
                    var query = from inv in db.InventoryItem where inv.UserId == userid select inv;
                    return query.ToList();
                }

            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");

            }
        }

        internal static List<Items> GetUserInvItems(List<InventoryItem> invList,int itemType)
        {
            List<Items> itemsList=new List<Items>();
            foreach(InventoryItem inv in invList)
            {
                for(int i = 0; i < inv.Quantity; i++)
                {
                    itemsList.Add(Items.GetItem(inv.ItemId, itemType));
                }
            }
            return itemsList;
        }
    }
}