using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public partial class PackageItems
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PackageId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        [ForeignKey("Item")]
        public int ItemName { get; set; }

        public int Quantity { get; set; }

		// Foreign key references
		public Items Item { get; set; }
	}
}