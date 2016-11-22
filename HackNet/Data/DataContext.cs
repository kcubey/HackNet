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
        public DbSet<Users> Users { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet<Missions> Missions { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Machines> Machines { get; set; }

        public DataContext() : base("DefaultConnection")
        {

        }
        
    }
}