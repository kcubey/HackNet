using HackNet.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;

namespace HackNet.Data
{
    public partial class Machines
    {
		[Key, ForeignKey("User")]
		public int UserId {get; set;}
		public string MachineName { get; set; }
        public string MachineProcessor { get; set; }
        public string MachineGraphicCard { get; set; }
        public string MachineMemory { get; set; }
        public string MachinePowerSupply { get; set; }
		public int Health{get; set;}
		public int Speed{get; set;}
		public int Attack{get; set;}
		public int Defence{get; set;}
		// Foreign key references
		public virtual Users User { get; set; }

        internal static void DefaultMachine(Users user, DataContext db)
        {
            Machines machines=new Machines();
            machines.UserId = user.UserID;
            machines.MachineName = user.UserName + "'s Machine";
            machines.MachineProcessor = "Intell® Atom™ x5-Z8300 Processor";
            machines.MachineGraphicCard = "Intell® HD Graphics";
            machines.MachineMemory = "DDR3L-RS 1600";
            machines.MachinePowerSupply = "CROSSSAIR VS Series™ VS550";
            machines.Health = 5;
            machines.Attack = 1;
            machines.Defence = 1;
            machines.Speed = 2;

            try
            {
                db.Machines.Add(machines);
                db.SaveChanges();
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");
            }
        }

        internal static Machines GetUserMachine(string username, DataContext db = null)
        {
            Machines machines;
            Users user = Users.FindUsername(username);
            try
            {
                if (db != null)
                {
                    
                }
            }
            catch(EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");

            }

            return null;
        }
        
        internal static void UpgradeUserMachine()
        {

        }
    }
}