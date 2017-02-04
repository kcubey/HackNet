using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HackNet.Game.Class
{
    public class MachineLogic
    {

        /// <summary>
        /// Update the Machine
        /// </summary>
        /// <param name="m"></param>
        internal static void UpdateMachine(Machines m)
        {
            using (DataContext db = new DataContext())
            {
                Machines mac = Machines.GetUserMachine(CurrentUser.Entity().UserID,db);
                // Name of Machine Part 
                mac.MachineProcessor = m.MachineProcessor;
                mac.MachineGraphicCard = m.MachineGraphicCard;
                mac.MachineMemory = m.MachineMemory;
                mac.MachinePowerSupply = m.MachinePowerSupply;

                // Part Bonus
                mac.Health = m.Health;
                mac.Attack = m.Attack;
                mac.Defence = m.Defence;
                mac.Speed = m.Speed;
                
                db.SaveChanges();
            }

        }

        
        /// <summary>
        /// Load Item into Machine Upgrade Panel
        /// </summary>
        /// <param name="ddList"></param>
        /// <param name="InvList"></param>
        /// <param name="itemType"></param>
        internal static void LoadItemIntoList(DropDownList ddList,List<Items> InvList,int itemType)
        {
            if (InvList.Count != 0)
            {
                ddList.Items.Add("===Select Upgrade===");
                foreach (Items i in InvList)
                {
                    if(i.ItemType==(ItemType)itemType)
                        ddList.Items.Add(new ListItem(i.ItemName, i.ItemBonus.ToString()));
                }
            }
            else
            {
                ddList.Items.Add("No Parts in Inventory");
                ddList.Enabled = false;
            }
        }

        /// <summary>
        /// Calculate probability for game
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        internal static int CalculateMachineLuck(Machines m)
        {
            double totalstat =200;
            double playerstat = m.Attack + m.Defence + m.Health + m.Speed;
            int ratio = (int)Math.Round((double)(100 * playerstat) / totalstat);
            return ratio;
        }

        internal static bool CheckInstalledParts(int UserID,int ItemID)
        {
            List<string> ChkPartList;
            using(DataContext db=new DataContext())
            {
                Machines m = Machines.GetUserMachine(UserID,db);
                ChkPartList = (from mac in db.Machines where mac.UserId == UserID select mac.MachineProcessor).ToList();
            }

            return false;
        }
    }
}