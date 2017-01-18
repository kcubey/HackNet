using HackNet.Data;
using HackNet.Game;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Admin
{
    public partial class MissionMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnAddMis_Click(object sender, EventArgs e)
        {
            MissionData misdata = new MissionData();
            misdata.MissionName = MisName.Text;
            misdata.MissionDesc = MisDesc.Text;
            misdata.MissionIP = Mission.GetRandomIp();
            misdata.MissionType = (MissionType)Int32.Parse(AtkTypeList.SelectedItem.Value);
            misdata.RecommendLevel = (RecommendLevel)Int32.Parse(RecomLvlList.SelectedItem.Value);
            misdata.MissionExp = int.Parse(MisExp.Text);
            misdata.MissionCoin = int.Parse(MisCoin.Text);
            using (DataContext db = new DataContext())
            {
                db.MissionData.Add(misdata);
                db.SaveChanges();
            }
            //LoadMissionList();
        }

        protected void AdminMissionView_Load(object sender, EventArgs e)
        {
            List<MissionData> misList = MissionData.GetMisList(-1);
            DataTable dt = new DataTable();
            dt.Columns.Add("MissionID", typeof(int));
            dt.Columns.Add("MissionName", typeof(string));
            dt.Columns.Add("MissionIP", typeof(string));
            foreach(MissionData m in misList)
            {
                dt.Rows.Add(m.MissionId,m.MissionName,m.MissionIP);
            }
            AdminMissionView.DataSource = dt;
            AdminMissionView.DataBind();


        }

        protected void EditMisBtn_Command(object sender, CommandEventArgs e)
        {

            MissionData m = MissionData.GetMissionData(int.Parse(e.CommandArgument.ToString()));
            EditMisID.Text = m.MissionId.ToString();
            EditMisName.Text = m.MissionName;
            EditIP.Text = m.MissionIP;
            EditMisType.Text = m.MissionType.ToString();
            EditMisDesc.Text = m.MissionDesc;


            ScriptManager.RegisterStartupScript(this, this.GetType(), "EditMissionModel", "showEditMissionModel()", true);

        }

        protected void UpdateMissionInfoBtn_Click(object sender, EventArgs e)
        {

        }
    }
}