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
			System.Diagnostics.Debug.WriteLine("Users table initializing");
			InitUsers().ForEach(s => context.Users.AddOrUpdate(s));
			context.SaveChanges();
		}

		private static List<Users> InitUsers()
		{
			var Users = new List<Users>();
			Users.Add(new Users
			{
				UserID = 1,
				UserName = "Wuggle",
				FullName = "Wen Liang",
				Email = "wenlianggg@gmail.com",
				Hash = new byte[0],
				Salt = Convert.FromBase64String("YK3q1SefESBO1YlwYWykXKQHYy7L/ZazkSQKxL8Hqt0BqA9MKd9SBgzzf1/uffQ/UkXzosJQqqeE7QKyMmXQYg=="),
				BirthDate = DateTime.Parse("1998-03-17"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Parse("2016-10-11")
			});
			Users.Add(new Users
			{
				UserID = 2,
				UserName = "RoyceFrost",
				FullName = "Roy Tang Qing Long",
				Email = "butterfrost90@gmail.com",
				Hash = new byte[0],
				Salt = Convert.FromBase64String("YK3q1SefESBO1YlwYWykXKQHYy7L/ZazkSQKxL8Hqt0BqA9MKd9SBgzzf1/uffQ/UkXzosJQqqeE7QKyMmXQYg=="),
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Parse("2016-10-11")
			});
			Users.Add(new Users
			{
				UserID = 3,
				UserName = "KeziaKew",
				FullName = "Kezia Kew",
				Email = "keziakew98@gmail.com",
				Hash = new byte[0],
				Salt = Convert.FromBase64String("YK3q1SefESBO1YlwYWykXKQHYy7L/ZazkSQKxL8Hqt0BqA9MKd9SBgzzf1/uffQ/UkXzosJQqqeE7QKyMmXQYg=="),
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Parse("2016-10-11")
			});
			Users.Add(new Users
			{
				UserID = 4,
				UserName = "DomSwag",
				FullName = "Dominic Gian",
				Email = "keeleyswag@gmail.com",
				Hash = new byte[0],
				Salt = Convert.FromBase64String("YK3q1SefESBO1YlwYWykXKQHYy7L/ZazkSQKxL8Hqt0BqA9MKd9SBgzzf1/uffQ/UkXzosJQqqeE7QKyMmXQYg=="),
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Parse("2016-10-11")
			});
			using (Authenticate auth = new Authenticate())
			{
				var bPassword = Encoding.UTF8.GetBytes("123");
				byte[] bDefaultHash = auth.Hash(bPassword, Users[0].Salt);
				foreach (Users u in Users)
				{
					u.Hash = bDefaultHash;
				}
			}
			System.Diagnostics.Debug.WriteLine("Users table initializing");
			return Users;

		}
	}
}
