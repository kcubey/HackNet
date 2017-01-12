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
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "HelpBtn", "showTutorial();", true);
            if (!IsPostBack)
            {
                using (DataContext db = new DataContext())
                {
                    // This is to add a new default machine.
                    // Machines.DefaultMachine(Authenticate.GetCurrentUser(),db);

                    Machines m = Machines.GetUserMachine(Authenticate.GetCurrentUser(), db);
                    Session["Machines"] = m;
                    Session["InvtoryList"] = ItemLogic.GetUserInvItems(Authenticate.GetCurrentUser(), -1);
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
                    // Upgrade Panel
                    WorkStnUpgradeName.Text = m.MachineName;
                    MachineLogic.LoadItemIntoList(ProcessList, 1);
                    MachineLogic.LoadItemIntoList(GraphicList, 4);
                    MachineLogic.LoadItemIntoList(MemoryList, 2);
                    MachineLogic.LoadItemIntoList(PowerSupList, 3);
                    CurrentProcessStatLbl.Text = m.Health.ToString();
                    CurrentGPUStatLbl.Text = m.Speed.ToString();
                    CurrentMemStatLbl.Text = m.Attack.ToString();
                    CurrentPowStatLbl.Text = m.Defence.ToString();
                }
            }
        }
       

        
        protected void UpgradeBtn_Click(object sender, EventArgs e)
        {
            Machines m = (Machines)Session["Machines"];

            System.Diagnostics.Debug.WriteLine("Current Pros: " + m.MachineProcessor);
            System.Diagnostics.Debug.WriteLine("Upgrade Pros: " + ProcessList.SelectedItem.Text);
            if (ProcessList.SelectedItem.Text != "===Select Upgrade===")
            {
               
                m.MachineProcessor = ProcessList.SelectedItem.Text;
                m.Health= int.Parse(ProcessList.SelectedValue);
            }
            if (GraphicList.SelectedItem.Text != "===Select Upgrade===")
            {
                m.MachineGraphicCard= GraphicList.SelectedItem.Text;
                m.Speed = int.Parse(GraphicList.SelectedValue);
            }
            if (MemoryList.SelectedItem.Text != "===Select Upgrade===")
            {
                m.MachineProcessor = MemoryList.SelectedItem.Text;
                m.Attack = int.Parse(MemoryList.SelectedValue);
            }
            if (PowerSupList.SelectedItem.Text != "===Select Upgrade===")
            {
                m.MachineProcessor = PowerSupList.SelectedItem.Text;
                m.Defence = int.Parse(PowerSupList.SelectedValue);
            }

            MachineLogic.UpdateMachine(m);
            Response.Redirect(Request.RawUrl);
        }

        protected void MarLnkBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Market1.aspx");
        }
    }
}