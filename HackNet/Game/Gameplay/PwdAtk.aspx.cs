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
    public partial class PwdAtk : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cache["Configure"] = false;
                Cache["PWDCalculated"] = false;
                Cache["Bypass"] = false;
                Cache["ScanList"] = Mission.scanMission((MissionData)Session["MissionData"], Authenticate.GetEmail(), false);
                LoadScanInfo((List<string>)Cache["ScanList"]);
            }
        }

        // This is to load a static output
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

        // this is to load the possible password list
        private void LoadPwdListToGrid(List<string> arrList)
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

        // This is to load the nautilus
        private void LoadNautilus(List<string> mlist)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Fname", typeof(string));
            dt.Columns.Add("LMD", typeof(DateTime));

            foreach(string m in mlist)
            {
                dt.Rows.Add(m, DateTime.Now);
            }
            NautilusView.DataSource = dt;
            NautilusView.DataBind();
        }

        // This is the command prompt that does everything
        protected void SubCmdBtn_Click(object sender, EventArgs e)
        {          
            if ((bool)Cache["Configure"])
            {
                if ((bool)Cache["PWDCalculated"] == false)
                {
                    if (CmdTextBox.Text == "run hydra")
                    {
                        List<string> pwdList = Mission.LoadPwdList();
                        Random rnd = new Random();
                        int r = rnd.Next(pwdList.Count);
                        System.Diagnostics.Debug.WriteLine("The answer is "+pwdList[r]);
                        Session["AnswerForPwd"] = pwdList[r];
                        Cache["PWDCalculated"] = true;
                        LoadScanInfo((List<string>)Cache["ScanList"]);
                        LoadPwdListToGrid(pwdList);
                        CmdError.Text = "hydra is running......";
                        CmdError.ForeColor = System.Drawing.Color.Green;
                        CmdTextBox.Text = string.Empty;
                    }
                    else
                    {
                        LoadScanInfo((List<string>)Cache["ScanList"]);
                        CmdError.Text = "Unrecognised Command";
                        CmdError.ForeColor = System.Drawing.Color.Red;
                        CmdTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    if ((bool)Cache["Bypass"] == false)
                    {
                        if (CmdTextBox.Text.Equals(Session["AnswerForPwd"].ToString()))
                        {
                            CmdError.Text = "Password Correct!";
                            CmdError.ForeColor = System.Drawing.Color.Green;
                            CmdTextBox.Text = string.Empty;
                            LoadScanInfo(Mission.LoadSuccessPwd((MissionData)Session["MissionData"]));
                            Cache["Bypass"] = true;
                        }
                        else
                        {
                            LoadScanInfo((List<string>)Cache["ScanList"]);
                            CmdError.Text = "Wrong Password";
                            CmdError.ForeColor = System.Drawing.Color.Red;
                            CmdTextBox.Text = string.Empty;
                        }
                    }else
                    {
                        if (CmdTextBox.Text.Equals("run nautilus"))
                        {
                            LoadScanInfo(Mission.LoadSuccessPwd((MissionData)Session["MissionData"], "run nautilus"));
                            // run method to load the datalist for nautilus
                            List<string> infoList = Mission.LoadNautilus();
                            LoadNautilus(infoList);
                    
                            CmdError.Text = "Nautilus is running....";
                            CmdError.ForeColor = System.Drawing.Color.Green;
                            CmdTextBox.Text = string.Empty;
                            CmdTextBox.Enabled = false;
                        }
                        else
                        {
                            LoadScanInfo(Mission.LoadSuccessPwd((MissionData)Session["MissionData"]));
                            CmdError.Text = "Unrecognised Command";
                            CmdError.ForeColor = System.Drawing.Color.Red;
                            CmdTextBox.Text = string.Empty;
                        }
                    }
                }
            }
            else
            {
                LoadScanInfo((List<string>)Cache["ScanList"]);
                CmdError.Text = "hydra has not been configure";
                CmdError.ForeColor = System.Drawing.Color.Red;
                CmdTextBox.Text = string.Empty;
            }
        }

        // this is to configure hydra
        protected void ConfigBtn_Click(object sender, EventArgs e)
        {
            string error = "Error input for: ";
            bool errorchk = false;
            MissionData mis = (MissionData)Session["MissionData"];
            // check if IP is configured correctly
            if (TargetIPLbl.Text != mis.MissionIP)
            {
                errorchk = true;
                error = error + "IP Address, ";
            }
            // check if target user is configured correctly
            if(!TargetTxtBox.Text.Equals("root"))
            {
                System.Diagnostics.Debug.WriteLine("This is an error"+ TargetTxtBox.Text);
                errorchk = true;
                error = error + "Wrong Target User";
            }

            if (errorchk == false)
            {
                // Set cannot edit
                TargetIPLbl.Enabled = false;
                TargetTxtBox.Enabled = false;
                TargetAtkTypeList.Enabled = false;
                // Set bool to true so that SubCmdBtn_Click() can check if hydra is configured
                Cache["Configure"] = true;
                
                LoadScanInfo((List<string>)Cache["ScanList"]);
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

        protected void StealLinkBtn_Command(object sender, CommandEventArgs e)
        {
            MissionData mis = (MissionData)Session["MissionData"];
            string stolenFile = e.CommandArgument.ToString();
            if (Mission.CheckStolenFile(stolenFile))
            {
                // Title
                SummaryTitle.Text = "Congratulations, Mission Completed!";
                SummaryTitle.ForeColor = System.Drawing.Color.Green;
                // Summary
                MisNameLbl.Text = mis.MissionName;
                MisIPLbl.Text = mis.MissionIP;
                MisSumLbl.Text = "";
                MisExpLbl.Text = "";
                MisCoinLbl.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "missionSumModel", "showFinishPrompt();", true);

            }
            else
            {
                // Title
                SummaryTitle.Text = "Mission Failed!";
                SummaryTitle.ForeColor = System.Drawing.Color.Red;


                ScriptManager.RegisterStartupScript(this, this.GetType(), "missionSumModel", "showFinishPrompt();", true);
            }
        }

        protected void ExitBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("Missions.aspx");
        }
    }
}