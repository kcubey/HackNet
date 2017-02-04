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
	public partial class Pack
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PackageId { get; set; }

		public string Description { get; set; }

        public decimal Price { get; set; }

        internal static List<Pack> GetPackageList()
        {
            List<Pack> pkgList = new List<Pack>();
            try
            {
                using (DataContext db = new DataContext())
                {
					var query = from i in db.Package select i;
					return query.ToList();
                }
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");
            }
        }

        internal static Pack GetPackage(int id)
        {
            Pack pkg = new Data.Pack();
            try
            {
                using (DataContext db = new DataContext())
                {
					pkg = (from i in db.Package where i.PackageId == id select i).FirstOrDefault();
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