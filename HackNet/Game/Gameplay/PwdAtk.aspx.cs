using HackNet.Data;
using HackNet.Game.Class;
using System;
using System.Collections.Generic;
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

            if (!IsPostBack)
            {
                LoadScanInfo(Mission.scanMission(mis, Context.User.Identity.Name, false));
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



        protected void CmdTextBox_TextChanged(object sender, EventArgs e)
        {
            if(CmdTextBox.Text=="run hydra")
            {
                List<string> arrList = Mission.scanMission(mis, Context.User.Identity.Name, false);
                List<string> pwdList = MissionPwdAtk.LoadPwdList();
                foreach(string s in pwdList)
                {
                    arrList.Add(s);
                }
                LoadScanInfo(arrList);
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

                ErrorLbl.ForeColor = System.Drawing.Color.Green;
                ErrorLbl.Text = "Successful Configurations";
            }
            else
            {
                LoadScanInfo(Mission.scanMission(mis, Context.User.Identity.Name, false));
                ErrorLbl.ForeColor = System.Drawing.Color.Red;
                ErrorLbl.Text = error;
            }
        }
    }
}