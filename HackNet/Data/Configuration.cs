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
				UserName = "User",
				FullName = "Wen Liang",
				Email = "1@2.co",
				Hash = new byte[0],
				Salt = Convert.FromBase64String("YK3q1SefESBO1YlwYWykXKQHYy7L/ZazkSQKxL8Hqt0BqA9MKd9SBgzzf1/uffQ/UkXzosJQqqeE7QKyMmXQYg=="),
				BirthDate = DateTime.Parse("1998-03-17"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Parse("2016-10-11")
			});
			using (Authenticate auth = new Authenticate())
			{
				var bPassword = Encoding.UTF8.GetBytes("123");
				Users[0].Hash = auth.Hash(bPassword, Users[0].Salt);
			}
			System.Diagnostics.Debug.WriteLine("Users table initializing");
			return Users;

		}
	}
}
