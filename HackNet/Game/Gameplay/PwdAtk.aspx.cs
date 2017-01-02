using HackNet.Data;
using HackNet.Game.Class;
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
        MissionData mis = MissionData.GetMissionData(1);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Cache["Configure"] = false;
                Cache["PWDCalculated"] = false;
                Cache["Bypass"] = false;
                Cache["ScanList"] = Mission.scanMission(mis, Authenticate.GetEmail(), false);
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
                    if ((bool)Cache["Bypass"] == false)
                    {
                        if (CmdTextBox.Text.Equals(Session["AnswerForPwd"].ToString()))
                        {
                            CmdError.Text = "Password Correct!";
                            CmdError.ForeColor = System.Drawing.Color.Green;
                            LoadScanInfo(Mission.LoadSuccessPwd(mis));
                            Cache["Bypass"] = true;
                        }
                        else
                        {
                            LoadScanInfo((List<string>)Cache["ScanList"]);
                            CmdError.Text = "Wrong Password";
                            CmdError.ForeColor = System.Drawing.Color.Red;
                        }
                    }else
                    {
                        if (CmdTextBox.Text.Equals("run nautilus"))
                        {
                            LoadScanInfo(Mission.LoadSuccessPwd(mis,"run nautilus"));
                            // run method to load the datalist for nautilus
                            List<string> infoList = Mission.LoadNautilus();
                            LoadNautilus(infoList);
                            StealInfo.Enabled = true;
                            DeleteInfo.Enabled = true;
                            CmdError.Text = "Nautilus is running....";
                            CmdError.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            CmdError.Text = "Unrecognised Command";
                            CmdError.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            else
            {
                LoadScanInfo((List<string>)Cache["ScanList"]);
                CmdError.Text = "hydra has not been configure";
                CmdError.ForeColor = System.Drawing.Color.Red;
            }
        }

        // this is to configure hydra
        protected void ConfigBtn_Click(object sender, EventArgs e)
        {
            string error = "Error input for: ";
            bool errorchk = false;

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

      
    }
}