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
                Response.Redirect("../Missions.aspx");
            }
            
            if (!IsPostBack)
            {
                ViewState["URLCalculated"] = false;
                ViewState["Bypass"] = false;
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

        private void LoadSQLList(DataList dl)
        {
            List<string> urlList = Mission.LoadSQLCode();
            DataTable dt = new DataTable();
            dt.Columns.Add("SQLCode", typeof(string));
            foreach (string s in urlList)
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
                ViewState["Configure"] = true;
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
                if((bool)ViewState["URLCalculated"] == false)
                {
                    if(CmdTextBox.Text=="run SQLInjection")
                    {
                        // Calculation of URL and picking correct URL for attack
                        List<string> urlList = Mission.LoadURLList();
                        Random rnd = new Random();
                        int r = rnd.Next(urlList.Count);
                        System.Diagnostics.Debug.WriteLine("The answer is " + urlList[r]);
                        ViewState["AnswerForURL"] = urlList[r];
                        ViewState["URLCalculated"] = true;

                        // Load messages and cmd panel
                        LoadScanInfo(ViewState["ScanList"] as List<string>);
                        LoadPossURLList(URLListView);
                        CmdError.Text = "SQLInjection is running......";
                        CmdError.ForeColor = System.Drawing.Color.Green;
                        CmdTextBox.Text = string.Empty;
                    }
                    else
                    {
                        // Checking for unrecognised
                        LoadScanInfo(ViewState["ScanList"] as List<string>);
                        CmdError.Text = "Unrecognised Command";
                        CmdError.ForeColor = System.Drawing.Color.Red;
                        CmdTextBox.Text = string.Empty;
                    }

                }
                else
                {
                    if ((bool)ViewState["Bypass"] == false)
                    {
                        if (CmdTextBox.Text.Equals(ViewState["AnswerForURL"].ToString()))
                        {
                            CmdError.Text = "URL is correct!";
                            CmdError.ForeColor = System.Drawing.Color.Green;
                            CmdTextBox.Text = string.Empty;
                            LoadScanInfo(Mission.LoadSuccessPwd((MissionData)Session["MissionData"]));
                            ViewState["Bypass"] = true;

                            // Enable the browser
                            UsrName.Enabled = true;
                            Password.Enabled = true;
                            LoadSQLList(SQLCodeList);
                        }
                        else
                        {
                            LoadScanInfo(ViewState["ScanList"] as List<string>);
                            CmdError.Text = "Wrong URL";
                            CmdError.ForeColor = System.Drawing.Color.Red;
                            CmdTextBox.Text = string.Empty;
                        }

                    }
                    else
                    {
                        CmdTextBox.Text = string.Empty;
                        CmdTextBox.Enabled = false;
                    }
                }
                

            }
            else
            {
                LoadScanInfo(ViewState["ScanList"] as List<string>);
                CmdError.Text = "SQLInjection not configured";
                CmdError.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UsrName.Text != "" || Password.Text != "")
            {
                if (UsrName.Text.Equals("adminbypass-'*/--") && Password.Text.Equals("' DROP ALL TABLES;--"))
                {
                    MissionData mis = (MissionData)Session["MissionData"];
                    // Title
                    SummaryTitle.Text = "Congratulations, Mission Completed!";
                    SummaryTitle.ForeColor = System.Drawing.Color.Green;
                    // Summary
                    MisNameLbl.Text = mis.MissionName;
                    MisIPLbl.Text = mis.MissionIP;
                    MisSumLbl.Text = "";
                    MisExpLbl.Text = mis.MissionExp.ToString();
                    MisCoinLbl.Text = mis.MissionCoin.ToString();

                    using (DataContext db = new DataContext())
                    {
                        Users u = Authenticate.GetCurrentUser(false, db);
                        u.TotalExp = u.TotalExp + mis.MissionExp;
                        System.Diagnostics.Debug.WriteLine("Total Exp: " + u.TotalExp);
                        db.SaveChanges();
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "missionSumModel", "showFinishPrompt();", true);
                }
                else
                {

                }
            }else
            {
                UsrName.Text = string.Empty;
                Password.Text = string.Empty;
                LoginErrorLbl.Text = "Error, please input SQL Code for BOTH fields";
                LoginErrorLbl.ForeColor = System.Drawing.Color.Red;

            }
        }
        protected void ExitBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Missions.aspx");
        }
    }
}