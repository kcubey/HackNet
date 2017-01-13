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

    }
}