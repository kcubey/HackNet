using HackNet.Data;
using HackNet.Game.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game.Gameplay
{
    public partial class PwdAtk : System.Web.UI.Page
    {
        MissionData mis = MissionData.GetMissionData(1);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Cache["Configure"] = false;
            if (!IsPostBack)
            {
                Cache["ScanList"] = Mission.scanMission(mis, Context.User.Identity.Name, false);
                LoadScanInfo((List<string>)Cache["ScanList"]);
            }

        }

        private void LoadScanInfo(List<string>arrList)
        {
            for (int i = 0; i < arrList.Count; i++)
            {
                Label lbl = new Label();
                lbl.Text = arrList[i].ToString();
                LogPanel.Controls.Add(lbl);
                LogPanel.Controls.Add(new LiteralControl("<br/>"));
            }
        }

        private void LoadPwdList(List<string> arrList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Posspwd", typeof(string));
            foreach (string pwd in arrList)
            {
                dt.Rows.Add(pwd);
            }
            PwdListView.DataSource = dt;
            PwdListView.DataBind();
        }

        protected void SubCmdBtn_Click(object sender, EventArgs e)
        {
            bool config = (bool)Cache["Configure"];
            if (config)
            {
                if (CmdTextBox.Text == "run hydra")
                {
                    List<string> arrList = (List<string>)Cache["ScanList"];
                    arrList.Add("List of possible passwords");
                    List<string> pwdList = MissionPwdAtk.LoadPwdList();
                    foreach (string s in pwdList)
                    {
                        arrList.Add(s);
                    }
                    LoadScanInfo(arrList);
                    LoadPwdList(pwdList);
                }
                else
                {
                    LoadScanInfo((List<string>)Cache["ScanList"]);
                    CmdError.Text = "Unrecognised Command";
                    CmdError.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LoadScanInfo((List<string>)Cache["ScanList"]);
                CmdError.Text = "hydra has not been configure";
                CmdError.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void ConfigBtn_Click(object sender, EventArgs e)
        {
            string error = "Error input for: ";
            bool errorchk = false;

            if (TargetIPLbl.Text != mis.MissionIP)
            {
                errorchk = true;
                error = error + "IP Address, ";
            }
            if(!TargetTxtBox.Text.Equals("root"))
            {
                System.Diagnostics.Debug.WriteLine("This is an error"+ TargetTxtBox.Text);
                errorchk = true;
                error = error + "Wrong Target User";
            }

            if (errorchk == false)
            {
                MissionPwdAtk misatk = new MissionPwdAtk();
                misatk.mis = mis;
                misatk.target = TargetTxtBox.Text;
                misatk.atkMethod = TargetAtkTypeList.Text;
                Session["MisAtk"] = misatk;
                // Set cannot edit
                TargetIPLbl.Enabled = false;
                TargetTxtBox.Enabled = false;
                TargetAtkTypeList.Enabled = false;
                Cache["Configure"] = true;
                ErrorLbl.ForeColor = System.Drawing.Color.Green;
                ErrorLbl.Text = "Successful Configurations";
            }
            else
            {
                LoadScanInfo((List<string>)Cache["ScanList"]);
                ErrorLbl.ForeColor = System.Drawing.Color.Red;
                ErrorLbl.Text = error;
            }
        }

      
    }
}