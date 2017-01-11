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
        internal static void UpdateMachine(Machines m)
        {
            using (DataContext db = new DataContext())
            {
                Machines mac = Machines.GetUserMachine(Authenticate.GetCurrentUser(),db);
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

                mac.UpgradeUserMachine(m);
                db.SaveChanges();
            }

        }

        // Load Item into Upgrade Panel
        internal static void LoadItemIntoList(DropDownList ddList, int itemType)
        {
            List<Items> itmList = ItemLogic.GetUserInvItems(Authenticate.GetCurrentUser(), itemType);
            if (itmList.Count != 0)
            {
                ddList.Items.Add("===Select Upgrade===");
                foreach (Items i in itmList)
                {
                    ddList.Items.Add(new ListItem(i.ItemName, i.ItemBonus.ToString()));
                }
            }
            else
            {
                ddList.Items.Add("No Parts in Inventory");
                ddList.Enabled = false;
            }
        }
    }
}