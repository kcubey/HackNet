using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackNet.Data
{
    public class InventoryItem
    {
		[Key, Column(Order = 1), ForeignKey("User")]
		public int UserId { get; set; }
		[Key, Column(Order = 2)]
		public int ItemId { get; set; }
		public int Quantity { get; set; }

		public Users User { get; set; }
    }
}