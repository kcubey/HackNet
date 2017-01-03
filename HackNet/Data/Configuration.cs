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
            /*
            if (!context.Items.Any())
				InitItems().ForEach(i => context.Items.AddOrUpdate(i));
                */
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
				AccessLevel = AccessLevel.Admin,
				BirthDate = DateTime.Parse("1998-03-17"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now
			});
			Users.Add(new Users
			{
				UserName = "RoyceFrost",
				FullName = "Roy Tang Qing Long",
				Email = "butterfrost90@gmail.com",
				AccessLevel = AccessLevel.Admin,
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now,
				Machine = new Machines
				{
					MachineName = "RoyceFrost's Machine",
					Health = 5,
					Speed = 2,
					Defence = 1,
					MachineProcessor = null,
					MachineGraphicCard = null,
					MachineMemory = null,
					MachinePowerSupply = null
				}
			});
			Users.Add(new Users
			{
				UserName = "KeziaKew",
				FullName = "Kezia Kew",
				Email = "keziakew98@gmail.com",
				AccessLevel = AccessLevel.Admin,
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now
			});
			Users.Add(new Users
			{
				UserName = "DomSwag",
				FullName = "Dominic Gian",
				Email = "keeleyswag@gmail.com",
				AccessLevel = AccessLevel.Admin,
				BirthDate = DateTime.Parse("1997-01-01"),
				Registered = DateTime.Parse("2016-10-10"),
				LastLogin = DateTime.Now
			});

			foreach (Users u in Users)
			{ 
				u.UpdatePassword("123");
			}
			System.Diagnostics.Debug.WriteLine("Users table initializing");
			return Users;
		}

		private static List<Items> InitItems()
		{
			var Items = new List<Items>();
			Items.Add(new Items
			{
				ItemName = "Intel® Core™ i7-7Y75 Processor",
				ItemType = ItemType.PartCpu,
				ItemBonus = 15,
				ItemDesc = "",
				ItemPrice = 0
			});
			Items.Add(new Items
			{
				ItemName = "myVidia GT720",
				ItemType = ItemType.PartGpu,
				ItemBonus = 15,
				ItemDesc = "",
				ItemPrice = 0
			});
			Items.Add(new Items
			{
				ItemName = "Corzair CX430",
				ItemType = ItemType.PartPower,
				ItemBonus = 15,
				ItemDesc = "",
				ItemPrice = 0
			});
			Items.Add(new Items
			{
				ItemName = "Generic DDR3 RAM",
				ItemType = ItemType.PartRam,
				ItemBonus = 15,
				ItemDesc = "",
				ItemPrice = 0

			});
			return Items;
		}

		private static List<Machines> InitMachines()
		{
			var Machines = new List<Machines>();
			Machines.Add(new Machines
			{

			});
			return Machines;
		}
	}
}
