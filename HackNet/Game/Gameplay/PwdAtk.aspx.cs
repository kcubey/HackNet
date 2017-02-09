using HackNet.Data;
using HackNet.Game.Class;
using HackNet.Loggers;
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
            if (Session["MissionData"] as MissionData == null)
            {
                Response.Redirect("../Missions.aspx");
            }
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HelpBtn", "showTutorial();", true);
                ViewState["Configure"] = false;
                ViewState["PWDCalculated"] = false;
                ViewState["Bypass"] = false;
                ViewState["ScanList"] = MissionLogic.scanMission((MissionData)Session["MissionData"], CurrentUser.GetEmail(), false);
                LoadScanInfo(ViewState["ScanList"] as List<string>);
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

        // This is to load the possible password list
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
            if ((bool)ViewState["Configure"])
            {
                if ((bool)ViewState["PWDCalculated"] == false)
                {
                    if (CmdTextBox.Text == "run hydra")
                    {
                        List<string> pwdList = MissionLogic.LoadPwdList();
                        Random rnd = new Random();
                        int r = rnd.Next(pwdList.Count);
                        System.Diagnostics.Debug.WriteLine("The answer is "+pwdList[r]);
                        ViewState["AnswerForPwd"] = pwdList[r];
                        ViewState["PWDCalculated"] = true;
                        LoadScanInfo(ViewState["ScanList"] as List<string>);
                        LoadPwdListToGrid(pwdList);
                        CmdError.Text = "hydra is running......";
                        CmdError.ForeColor = System.Drawing.Color.Green;
                        CmdTextBox.Text = string.Empty;
                        Step2Lbl.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
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
                        string inputCommand = CmdTextBox.Text;
                        string AnswerCommnad = "su -root -P " + ViewState["AnswerForPwd"].ToString();
                        if (inputCommand.Equals(AnswerCommnad))
                        {
                            CmdError.Text = "Password Correct!";
                            CmdError.ForeColor = System.Drawing.Color.Green;
                            CmdTextBox.Text = string.Empty;
                            LoadScanInfo(MissionLogic.LoadSuccessPwd((MissionData)Session["MissionData"]));
                            ViewState["Bypass"] = true;
                            Step3Lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            LoadScanInfo(ViewState["ScanList"] as List<string>);
                            CmdError.Text = "Wrong Password";
                            CmdError.ForeColor = System.Drawing.Color.Red;
                            CmdTextBox.Text = string.Empty;
                        }
                    }else
                    {
                        if (CmdTextBox.Text.Equals("run nautilus"))
                        {
                            LoadScanInfo(MissionLogic.LoadSuccessPwd((MissionData)Session["MissionData"], "run nautilus"));
                            // run method to load the datalist for nautilus
                            List<string> infoList = MissionLogic.LoadNautilus();
                            LoadNautilus(infoList);
                    
                            CmdError.Text = "Nautilus is running....";
                            CmdError.ForeColor = System.Drawing.Color.Green;
                            CmdTextBox.Text = string.Empty;
                            CmdTextBox.Enabled = false;
                            Step4Lbl.ForeColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            LoadScanInfo(MissionLogic.LoadSuccessPwd((MissionData)Session["MissionData"]));
                            CmdError.Text = "Unrecognised Command";
                            CmdError.ForeColor = System.Drawing.Color.Red;
                            CmdTextBox.Text = string.Empty;
                        }
                    }
                }
            }
            else
            {
                LoadScanInfo(ViewState["ScanList"] as List<string>);
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
                ViewState["Configure"] = true;
                
                LoadScanInfo(ViewState["ScanList"] as List<string>);
                ErrorLbl.ForeColor = System.Drawing.Color.Green;
                ErrorLbl.Text = "Successful Configurations";
                Step1Lbl.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                LoadScanInfo(ViewState["ScanList"] as List<string>);
                ErrorLbl.ForeColor = System.Drawing.Color.Red;
                ErrorLbl.Text = error;

            }
        }

        protected void StealLinkBtn_Command(object sender, CommandEventArgs e)
        {
            MissionData mis = (MissionData)Session["MissionData"];

            string stolenFile = e.CommandArgument.ToString();
            if (mis.MissionType == 0)
            {
                if (MissionLogic.CheckStolenFile(stolenFile))
                {
                    // Title
                    SummaryTitle.Text = "Congratulations, Mission Completed!";
                    SummaryTitle.ForeColor = System.Drawing.Color.Green;
                    // Summary
                    MisNameLbl.Text = mis.MissionName;
                    MisIPLbl.Text = mis.MissionIP;
                    MisSumLbl.Text = "Some perpetrators uses these methods to gain access to companies to actually steal theri client information. As such, this could be prevented if firewall rules and other configurations were set up correctly.";
                    MisExpLbl.Text = mis.MissionExp.ToString();
                    MisCoinLbl.Text = mis.MissionCoin.ToString();
                    Step5Lbl.ForeColor = System.Drawing.Color.Green;
                    

                    using (DataContext db = new DataContext())
                    {
                        Users u = CurrentUser.Entity(false, db);
                        u.TotalExp = u.TotalExp + mis.MissionExp;
                        
                        Items i = ItemLogic.GetRewardForMis(mis.RecommendLevel, Machines.GetUserMachine(CurrentUser.Entity().UserID,db));
                        ItemNameLbl.Text = i.ItemName;
                        ItemBonusLbl.Text = i.ItemBonus.ToString();
                        ItemImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);

                        InventoryItem invItem;
                        if (ItemLogic.CheckInventoryItem(db, u.UserID, i.ItemId, out invItem))
                        {
                            invItem.Quantity += 1;
                            db.SaveChanges();
                        }
                        else
                        {
                            invItem = new InventoryItem();
                            invItem.UserId = u.UserID;
                            invItem.ItemId = i.ItemId;
                            invItem.Quantity = 1;
                            db.InventoryItem.Add(invItem);
                        }
                        List<string> RewardList = new List<string>();
                        RewardList.Add("Mission Exp: "+mis.MissionExp.ToString());
                        RewardList.Add("Mission Coin: "+mis.MissionCoin.ToString());
                        RewardList.Add("Item: "+i.ItemName);

                        MissionLogLogic.Store(CurrentUser.Entity().UserID,mis.MissionName,true, RewardList);

                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "missionSumModel", "showFinishPrompt();", true);

                }
                else
                {
                    // Title
                    SummaryTitle.Text = "Mission Failed!";
                    SummaryTitle.ForeColor = System.Drawing.Color.Red;
                    MisNameLbl.Text = mis.MissionName;
                    MisIPLbl.Text = mis.MissionIP;
                    MisSumLbl.Text = "Mission Failed due to incorrect file choosen.";
                    MisExpLbl.Text = "0";
                    MisCoinLbl.Text = "0";
                    Step5Lbl.ForeColor = System.Drawing.Color.Red;
                    ItemImage.Visible = false;
                    List<string> rewardList = new List<string>();
                    rewardList.Add("Failed Mission no reward");
                    MissionLogLogic.Store(CurrentUser.Entity().UserID, mis.MissionName, false,rewardList);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "missionSumModel", "showFinishPrompt();", true);
                }
            }else
            {
                // Title
                SummaryTitle.Text = "Mission Failed!";
                SummaryTitle.ForeColor = System.Drawing.Color.Red;
                MisNameLbl.Text = mis.MissionName;
                MisIPLbl.Text = mis.MissionIP;
                MisSumLbl.Text = "Mission Failed due to incorrect type of attack choosen";
                MisExpLbl.Text = "0";
                MisCoinLbl.Text = "0";
                Step5Lbl.ForeColor = System.Drawing.Color.Red;
                ItemImage.Visible = false;
                List<string> rewardList = new List<string>();
                rewardList.Add("Failed Mission no reward");
                MissionLogLogic.Store(CurrentUser.Entity().UserID, mis.MissionName, false, rewardList);


                ScriptManager.RegisterStartupScript(this, this.GetType(), "missionSumModel", "showFinishPrompt();", true);
            }
        }

        protected void ExitBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Missions.aspx");
        }
    }
}