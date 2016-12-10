using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace HackNet.Data
{
	public class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

		protected override void Seed(DataContext context)
		{
			if (!context.Users.Any())
				InitUsers().ForEach(s => context.Users.AddOrUpdate(s));
			context.SaveChanges();
			System.Diagnostics.Debug.WriteLine("Seed method execution complete");
		}

		private static List<Users> InitUsers()
		{
			var Users = new List<Users>();
			Users.Add(new Users
			{
				UserName = "Wuggle",
				FullName = "Wen Liang",
				Email = "wenlianggg@gmail.com",
				BirthDate = DateTime.Parse("1998-03-17"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now,
				UserKeyStore = new UserKeyStore
				{
					UserId = 1,
					RsaPub = new byte[0],
					RsaPriv = new byte[0],
				}
			});
			Users.Add(new Users
			{
				UserName = "RoyceFrost",
				FullName = "Roy Tang Qing Long",
				Email = "butterfrost90@gmail.com",
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now,
				UserKeyStore = new UserKeyStore
				{
					UserId = 1,
					RsaPub = new byte[0],
					RsaPriv = new byte[0],
				}
			});
			Users.Add(new Users
			{
				UserName = "KeziaKew",
				FullName = "Kezia Kew",
				Email = "keziakew98@gmail.com",
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now,
				UserKeyStore = new UserKeyStore
				{
					UserId = 1,
					RsaPub = new byte[0],
					RsaPriv = new byte[0],
				}
			});
			Users.Add(new Users
			{
				UserName = "DomSwag",
				FullName = "Dominic Gian",
				Email = "keeleyswag@gmail.com",
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now,
				UserKeyStore = new UserKeyStore
				{
					UserId = 1,
					RsaPub = new byte[0],
					RsaPriv = new byte[0],
				}
			});

			using (Authenticate auth = new Authenticate())
			{
				foreach (Users u in Users)
				{
					u.UpdatePassword("123", null);
				}
			}
			System.Diagnostics.Debug.WriteLine("Users table initializing");
			return Users;

		}
	}
}
