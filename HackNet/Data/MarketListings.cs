using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public partial class MarketListings
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ListingId { get; set; }

		public int ItemId { get; set; }

        public string ItemTitle { get; set; }

        public string Description { get; set; }

		public double Price { get; set; }
	}
}