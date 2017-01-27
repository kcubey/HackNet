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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "HelpBtn", "showTutorial();", true);
            using (DataContext db = new DataContext())
            {
                // Machines.DefaultMachine(CurrentUser.Entity(), db);
                Machines m = Machines.GetUserMachine(CurrentUser.Entity(),db);
                Session["Machines"] = m;
                List<Items> InvItmList = ItemLogic.GetUserInvItems(CurrentUser.Entity(), -1,db);

                // Store into Encrypted Viewstate
                ViewState["InvetoryList"] = InvItmList;

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
                MachineLogic.LoadItemIntoList(ProcessList, InvItmList, 1);
                MachineLogic.LoadItemIntoList(GraphicList, InvItmList, 4);
                MachineLogic.LoadItemIntoList(MemoryList, InvItmList, 2);
                MachineLogic.LoadItemIntoList(PowerSupList, InvItmList, 3);

                CurrentProcessStatLbl.Text = m.Health.ToString();
                CurrentGPUStatLbl.Text = m.Speed.ToString();
                CurrentMemStatLbl.Text = m.Attack.ToString();
                CurrentPowStatLbl.Text = m.Defence.ToString();
            }

        }



        protected void UpgradeBtn_Click(object sender, EventArgs e)
        {

            Machines m = Session["Machines"] as Machines;
            List<Items> invItemList = ViewState["InvetoryList"] as List<Items>;

            if (ProcessList.SelectedItem.Text != "===Select Upgrade===")
            {
                m.MachineProcessor = ProcessList.SelectedItem.Text;
                m.Health = int.Parse(ProcessList.SelectedValue);
            }
            if (GraphicList.SelectedItem.Text != "===Select Upgrade===")
            {
                m.MachineGraphicCard = GraphicList.SelectedItem.Text;
                m.Speed = int.Parse(GraphicList.SelectedValue);
            }
            if (MemoryList.SelectedItem.Text != "===Select Upgrade===")
            {
                m.MachineMemory = MemoryList.SelectedItem.Text;
                m.Attack = int.Parse(MemoryList.SelectedValue);
            }
            if (PowerSupList.SelectedItem.Text != "===Select Upgrade===")
            {
                m.MachinePowerSupply = PowerSupList.SelectedItem.Text;
                m.Defence = int.Parse(PowerSupList.SelectedValue);
            }

            MachineLogic.UpdateMachine(m);
            Response.Redirect(Request.RawUrl);
        }

        protected void MarLnkBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Market.aspx");
        }
    }
}