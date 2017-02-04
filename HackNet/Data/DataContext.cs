using HackNet.Data;
using HackNet.Game;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace HackNet.Data
{
    public class DataContext : DbContext
    {

		private static int ctr;

		public DataContext() : base("DefaultConnection")
		{
			System.Diagnostics.Debug.WriteLine("DATACONTEXT ACCESSED (" + ++ctr + ")");
			Configuration.ProxyCreationEnabled = true;
			Configuration.LazyLoadingEnabled = true;
		}


		public DbSet<Users> Users { get; set; }
		public DbSet<UserKeyStore> UserKeyStore { get; set; }
        // public DbSet<UserIPList> UserIPList { get; set; }
		public DbSet<Confirmations> Confirmations { get; set; }
		public DbSet<Logs> Logs { get; set; }
		public DbSet<MissionLog> MissionLog { get; set; }
		public DbSet<Machines> Machines { get; set; }
		public DbSet<MissionData> MissionData { get; set; }
        public DbSet<AttackData> AttackData { get; set; }
		public DbSet<Conversation> Conversation { get; set; }
        public DbSet<SecureMessage> SecureMessage { get; set; }
		public DbSet<Items> Items { get; set; }
		public DbSet<InventoryItem> InventoryItem { get; set; }
		public DbSet<Pack> Package { get; set; }
		public DbSet<PackItem> PackItem { get; set; }
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

			// Conversations
			modelBuilder.Entity<Conversation>()
						.HasRequired(conv => conv.UserB)
						.WithMany(u => u.Conversations)
						.WillCascadeOnDelete(false);

			// SecureMessage
			modelBuilder.Entity<SecureMessage>()
						.HasRequired(m => m.Conversation)
						.WithMany()
						.WillCascadeOnDelete(false);

		}



	}
}