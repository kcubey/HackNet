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
	public partial class Packages
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PackageId { get; set; }

		public string Description { get; set; }

        public double Price { get; set; }

        internal static List<Packages> GetPackageList()
        {
            List<Packages> pkgList = new List<Packages>();
            try
            {
                using (DataContext db = new DataContext())
                {
					var query = from i in db.Packages select i;
					return query.ToList();
                }
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");
            }
        }

        internal static Packages GetPackage(int id)
        {
            Packages pkg = new Data.Packages();
            try
            {
                using (DataContext db = new DataContext())
                {
                    pkg = (from i in db.Packages where i.PackageId == id select i).FirstOrDefault();
                }
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");
            }
            return pkg;
        }

    }
}