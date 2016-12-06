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
		public DbSet<UserKeyStore> UserKeyStore { get; set; }
		public DbSet<Logs> Logs { get; set; }
		public DbSet<Machines> Machines { get; set; }
		public DbSet<MissionData> MissionData { get; set; }
		public DbSet<Messages> Messages { get; set; }
		public DbSet<Items> Items { get; set; }
		public DbSet<InventoryItem> InventoryItem { get; set; }
		public DbSet<Packages> Packages { get; set; }
		public DbSet<PackageItems> PackageItems { get; set; }
		public DbSet<MarketListings> MarketListings { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Users
			modelBuilder.Entity<Users>()
						.HasOptional(us => us.Machine)
						.WithRequired(mch => mch.User)
						.WillCascadeOnDelete(false);
			modelBuilder.Entity<Users>()
						.HasOptional(usr => usr.UserKeyStore)
						.WithRequired(ks => ks.User)
						.WillCascadeOnDelete(false);


			// Items
			modelBuilder.Entity<InventoryItem>()
						.HasRequired(own => own.User)
						.WithMany(ownu => ownu.Inventory)
						.HasForeignKey(ifk => ifk.UserId)
						.WillCascadeOnDelete(false);

			// Messages
			modelBuilder.Entity<Messages>()
						.HasRequired(ms => ms.Sender)
						.WithMany(snd => snd.SentMessages)
						.HasForeignKey(msf => msf.SenderId)
						.WillCascadeOnDelete(false);
			modelBuilder.Entity<Messages>()
						.HasRequired(mr => mr.Receiver)
						.WithMany(rcv => rcv.ReceivedMessages)
						.HasForeignKey(mrf => mrf.ReceiverId)
						.WillCascadeOnDelete(false);

		}



	}
}