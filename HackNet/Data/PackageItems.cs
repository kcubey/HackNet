using HackNet.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
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

        public int Quantity { get; set; }

		// Foreign key references
		public Items Item { get; set; }

        internal static PackageItems GetPackageItems(int pkgId)
        {
            PackageItems pkgItems = new Data.PackageItems();
            try
            {
                using (DataContext db = new DataContext())
                {
                    pkgItems = (from i in db.PackageItems where i.PackageId == pkgId select i).FirstOrDefault();
                }
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");
            }
            return pkgItems;
        }
    }
}