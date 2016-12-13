using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}