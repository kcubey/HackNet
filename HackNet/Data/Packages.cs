using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public partial class Packages
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PackageId { get; set; }

		public string Description { get; set; }

        public byte[] PackagePic { get; set; }

        public double Price { get; set; }
	}
}