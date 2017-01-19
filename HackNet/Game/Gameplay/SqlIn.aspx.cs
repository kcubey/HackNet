using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game.Gameplay
{
    public partial class SqlIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["MissionData"] as MissionData == null)
            {
                // prevent path travesel?
                Response.Redirect("../Missions.aspx");
            }
            
            if (!IsPostBack)
            {
                ViewState["Configure"] = false;
                ViewState["ScanList"] = Mission.scanMission((MissionData)Session["MissionData"], Authenticate.GetEmail(), false);
                LoadScanInfo(ViewState["ScanList"] as List<string>);
            }
        }

        // This is to load a static output
        private void LoadScanInfo(List<string> arrList)
        {
            for (int i = 0; i < arrList.Count; i++)
            {
                Label lbl = new Label();
                lbl.Text = arrList[i].ToString();
                LogPanel.Controls.Add(lbl);
                LogPanel.Controls.Add(new LiteralControl("<br/>"));
            }
        }

        // This is to load the possible URL for attack
        private void LoadPossURLList(DataList dl)
        {
            List<string> urlList = Mission.LoadURLList();
            DataTable dt = new DataTable();
            dt.Columns.Add("PossURL", typeof(string));
            foreach(string s in urlList)
            {
                dt.Rows.Add(s);
            }
            dl.DataSource = dt;
            dl.DataBind();
        }

        protected void ConfigSQL_Click(object sender, EventArgs e)
        {
            MissionData m = Session["MissionData"] as MissionData;

            if (TargetIPTxtBox.Text.Equals(m.MissionIP))
            {
                ErrorLbl.Text = "Successful Configurations";
                ErrorLbl.ForeColor = System.Drawing.Color.Green;
                LoadScanInfo(ViewState["ScanList"] as List<string>);
            }
            else
            {
                ErrorLbl.Text = "Wrong IP address entered";
                ErrorLbl.ForeColor = System.Drawing.Color.Red;
                LoadScanInfo(ViewState["ScanList"] as List<string>);
            }
        }

        protected void SubCmdBtn_Click(object sender, EventArgs e)
        {
            if ((bool)ViewState["Configure"])
            {
                LoadScanInfo(ViewState["ScanList"] as List<string>);
                LoadPossURLList(URLListView);
            }
            else
            {
                LoadScanInfo(ViewState["ScanList"] as List<string>);
                CmdError.Text = "";
                CmdError.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {

        }
    }
}