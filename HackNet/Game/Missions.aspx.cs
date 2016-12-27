using HackNet.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        }

        protected void regatkList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int recomLvl = Int32.Parse(regatkList.SelectedValue);
            if (recomLvl == -1)
            {
                AtkListView.DataSource = null;
                AtkListView.DataBind();
            }
            LoadMissionList(recomLvl);
        }
        private void LoadMissionList(int recomLvl)
        {
            List<MissionData> misdatalist = MissionData.GetMisList(recomLvl);

            dtMission = new DataTable();
            dtMission.Columns.Add("MissionId",typeof(int));
            dtMission.Columns.Add("IP Address", typeof(string));
            dtMission.Columns.Add("Mission Name", typeof(string));
            dtMission.Columns.Add("Recommended Level", typeof(string));
                                
            foreach (MissionData misdata in misdatalist)
            {
                dtMission.Rows.Add(misdata.MissionId,misdata.MissionIP, misdata.MissionName, misdata.RecommendLevel);
            }

            AtkListView.DataSource = dtMission;
            AtkListView.DataBind();
        }

        protected void ViewMis_Command(object sender, CommandEventArgs e)
        {
            MissionData mis = MissionData.GetMissionData(Int32.Parse(e.CommandArgument.ToString()));
            MissionTitleLbl.Text = mis.MissionName;
            MisDesLbl.Text = mis.MissionDesc;



            ScriptManager.RegisterStartupScript(this, this.GetType(), "attackSummaryModel", "showPopupattacksummary();", true);
        }

        protected void AttackLink_Click(object sender, EventArgs e)
        {
            MissionData mis = new MissionData();
            List<string> arrList = Mission.scanMission(mis,"testuser");

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

        // This is to display about Attack information
        protected void abtAtkInfo_Command(object sender, CommandEventArgs e)
        {          
            System.Diagnostics.Debug.WriteLine("testing:" + e.CommandArgument.ToString());
            AttackTypeHeaderLbl.Text = e.CommandArgument.ToString();
            

            ScriptManager.RegisterStartupScript(this, this.GetType(), "attackTypeModel", "showPopupattackinfo();", true);
        }


        // For temp only
        protected void btnAddMis_Click(object sender, EventArgs e)
        {
            MissionData misdata = new MissionData();
            misdata.MissionName = MisName.Text;
            misdata.MissionDesc = MisDesc.Text;
            misdata.MissionIP = Mission.GetRandomIp();
            misdata.MissionType = (MissionType)Int32.Parse(AtkTypeList.SelectedItem.Value);
            misdata.RecommendLevel = (RecommendLevel)Int32.Parse(RecomLvlList.SelectedItem.Value);
            using(DataContext db=new DataContext())
            {
                db.MissionData.Add(misdata);
                db.SaveChanges();
            }
            //LoadMissionList();
        }

        protected void btnAtkInfo_Click(object sender, EventArgs e)
        {
            AttackData atkdata = new AttackData();
            atkdata.AttackName = AtkName.Text;
            atkdata.AttackInfo = AtkInfo.Text;

            Stream strm = UploadAttack1.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(strm);
            atkdata.AttackPic1 = br.ReadBytes((int)strm.Length);

        }
    }
}