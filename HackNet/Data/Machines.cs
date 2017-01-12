using HackNet.Security;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Collections.Generic;
using HackNet.Game.Class;

namespace HackNet.Data
{
    public partial class Machines
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public string MachineName { get; set; }
        public string MachineProcessor { get; set; }
        public string MachineGraphicCard { get; set; }
        public string MachineMemory { get; set; }
        public string MachinePowerSupply { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        // Foreign key references
        public virtual Users User { get; set; }


        // Creates a new machine
        internal static void DefaultMachine(Users user, DataContext db)
        {
            Machines machines = new Machines();
            machines.UserId = user.UserID;
            machines.MachineName = user.UserName + "'s Machine";
            List<Items> defaultItemList = ItemLogic.GetDefaultParts();

            machines.MachineProcessor = defaultItemList[0].ItemName;
            machines.MachineGraphicCard = defaultItemList[1].ItemName;
            machines.MachineMemory = defaultItemList[2].ItemName;
            machines.MachinePowerSupply = defaultItemList[3].ItemName;
            machines.Health = defaultItemList[0].ItemBonus;
            machines.Attack = defaultItemList[1].ItemBonus;
            machines.Defence = defaultItemList[2].ItemBonus;
            machines.Speed = defaultItemList[3].ItemBonus;

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

        internal static Machines GetUserMachine(Users user, DataContext db)
        {
            Machines machines;          
            try
            {
                machines = (from m in db.Machines
                            where m.UserId == user.UserID
                            select m).FirstOrDefault();
            }
            catch (EntityCommandExecutionException)
            {
                throw new ConnectionException("Database link failure has occured");

            }

            return machines;
        }
        
    }
}