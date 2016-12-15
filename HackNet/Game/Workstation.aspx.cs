using HackNet.Data;
using HackNet.Game.Class;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Workstation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (DataContext db = new DataContext()) {
                //Machines.DefaultMachine(Authenticate.GetCurrentUser(),db);
                Machines m=Machines.GetUserMachine(Authenticate.GetCurrentUser(), db);
                // Text Labels
                WorkstationNameLbl.Text = m.MachineName;
                ProcessorLbl.Text = m.MachineProcessor;
                GraphicLbl.Text = m.MachineGraphicCard;
                MemoryLbl.Text = m.MachineMemory;
                PwsupLbl.Text = m.MachinePowerSupply;
                // Attribute Labels
                HpattrLabel.Text = m.Health.ToString();
                AtkattrLabel.Text = m.Attack.ToString();
                DefattrLabel.Text = m.Defence.ToString();
                SpeedattrLabel.Text = m.Speed.ToString();

            }


        }

    }
}