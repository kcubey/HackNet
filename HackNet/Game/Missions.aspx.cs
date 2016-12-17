using HackNet.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Missions : System.Web.UI.Page
    {
        DataTable dtMission;
        protected void Page_Load(object sender, EventArgs e)
        {
            regatkList.DataSource = getRegAtkList();
            regatkList.DataBind();
            LoadMissionList();
        }

        private List<string> getRegAtkList()
        {
            List<string> atkList = new List<string>();
            atkList.Add("Local");
            atkList.Add("America");
            atkList.Add("Asia");
            return atkList;
        }
        
        private void LoadMissionList()
        { 
            /*MissionData misData = MissionData.GetMis();
            
            IDataReader data = (IDataReader)misData;
            dtMission.Load(data);
            AtkTableView.DataSource = dtMission;
            AtkTableView.DataBind();
            */
        }
        protected void AttackLink_Click(object sender, EventArgs e)
        {
            Mission mis = new Mission();
            mis.IPaddress = Mission.GetRandomIp();
            mis.Objective = "Steal Data";
            mis.numOfPorts = 10;

            List<string> arrList = mis.scanMission(mis,"testuser");

            for(int i=0;i<arrList.Count;i++)
            {
                Label lbl = new Label();
                lbl.Text = arrList[i].ToString();
                LogPanel.Controls.Add(lbl);
                LogPanel.Controls.Add(new LiteralControl("<br/>"));
            }

            
        }

      

        protected void AtkTextBx_TextChanged(object sender, EventArgs e)
        {
            string attackType=AtkTextBx.Text;
            if (checkMissionType(attackType))
            {

            }else
            {
                errorLbl.Text = "THIS IS WRONG TRY AGAIN";
            }
        }
        internal bool checkMissionType(string atkType)
        {
            if (atkType.Equals("PWDAtk"))
                return true;
            if (atkType.Equals("DDOS"))
                return true;
            if (atkType.Equals("MITM"))
                return true;
            if (atkType.Equals("SQL"))
                return true;
            if (atkType.Equals("XXS"))
                return true;

            return false;
        }
        // this is to display about attack information
        protected void abtAtkInfo_Command(object sender, CommandEventArgs e)
        {          
            System.Diagnostics.Debug.WriteLine("testing:" + e.CommandArgument.ToString());
            AttackTypeHeader.Text = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "attackTypeModel", "showPopupattackinfo();", true);
        }

        protected void ViewVicBtn_Command(object sender, CommandEventArgs e)
        {

        }

        protected void btnAddMis_Click(object sender, EventArgs e)
        {
            MissionData misdata = new MissionData();
            misdata.MissionName = MisName.Text;
            misdata.MissionType = (MissionType)Int32.Parse(AtkTypeList.SelectedItem.Value);
            misdata.RecommendLevel = (RecommendLevel)Int32.Parse(RecomLvlList.SelectedItem.Value);
            using(DataContext db=new DataContext())
            {
                db.MissionData.Add(misdata);
                db.SaveChanges();
            }
        }
    }
}