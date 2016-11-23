using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace HackNet.Data
{
	internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
			ContextKey = "HackNet.Data.DataContext";
        }

		protected override void Seed(DataContext context)
		{
			System.Diagnostics.Debug.WriteLine("Users table initializing");
			InitUsers().ForEach(s => context.Users.Add(s));
			context.SaveChanges();
		}

		private static List<Users> InitUsers()
		{
			var Users = new List<Users>();
			Users.Add(new Users
			{
				UserName = "LoserGamer",
				FullName = "Wen Liang",
				Email = "wugglelord@gmail.com",
				Hash = "SomeHashHere",
				Salt = "SomeSaltHere",
				BirthDate = DateTime.Parse("1998-03-17"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Parse("2016-10-11"),
				isAdmin = true
			});
			System.Diagnostics.Debug.WriteLine("Users table initializing");
			return Users;

		}
	}
}
