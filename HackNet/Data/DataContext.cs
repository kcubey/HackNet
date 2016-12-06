using HackNet.Data;
using HackNet.Game;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
    public class DataContext : DbContext
    {
		public DataContext() : base("DefaultConnection")
		{
			System.Diagnostics.Debug.WriteLine("Data context created");
		}


		public DbSet<Users> Users { get; set; }

		public DbSet<Logs> Logs { get; set; }

		public DbSet<Machines> Machines { get; set; }

		public DbSet<MissionLogs> MissionLogs { get; set; }

		public DbSet<MissionData> MissionData { get; set; }

		public DbSet<InventoryItem> InventoryItem { get; set; }

    }
}