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
	public partial class PackItem
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PackageId { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }

        public int Quantity { get; set; }

		// Foreign key references
		public Items Item { get; set; }

        internal static PackItem GetPackageItems(int pkgId,bool ReadOnly,DataContext db=null)
        {
            if (ReadOnly == true)
            {
                PackItem pkgItems = new Data.PackItem();

                using (DataContext db1 = new DataContext())
                {
                    pkgItems = (from p in db1.PackItem where p.PackageId == pkgId select p).FirstOrDefault();
                }
                return pkgItems;
            }else
            {
                PackItem pkgItems;
                pkgItems= (from p in db.PackItem where p.PackageId == pkgId select p).FirstOrDefault();
                return pkgItems;
            }
            
        }
    }
}