using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public partial class Items
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ItemId { get; set; }
		public string ItemName { get; set; }
		public ItemType ItemType { get; set; }
        public int ItemBonus { get; set; }
	}

	public enum ItemType
	{
		PartCpu = 1,
		PartRam = 2,
		PartPower = 3,
		PartGpu = 4,
		Bonus = 0
	}

	public static class ItemTypeExtension {

		public static bool IsPart(this ItemType it)
		{
			switch(it)
			{
				case ItemType.PartCpu:
					return true;
				case ItemType.PartGpu:
					return true;
				case ItemType.PartPower:
					return true;
				case ItemType.PartRam:
					return true;
				case ItemType.Bonus:
					return false;
				default:
					return false;
			}
		}
	}
}