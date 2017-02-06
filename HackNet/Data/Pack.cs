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

        internal static Pack GetPackage(int id,bool ReadOnly,DataContext db=null)
        {
            if (ReadOnly == true)
            {
                Pack pkg = new Data.Pack();

                using (DataContext db1 = new DataContext())
                {
                    pkg = (from p in db1.Package where p.PackageId == id select p).FirstOrDefault();
                }

                return pkg;
            }
            else
            {
                Pack pkg;
                pkg= (from p in db.Package where p.PackageId == id select p).FirstOrDefault();
                return pkg;
            }
        }

    }
}