using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Missions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            regatkList.DataSource = getRegAtkList();
            regatkList.DataBind();

        }

        private List<string> getRegAtkList()
        {
            List<string> atkList = new List<string>();
            atkList.Add("Local");
            atkList.Add("America");
            return atkList;
        }

        protected void AttackLink_Click(object sender, EventArgs e)
        {
            Mission mis = new Mission();
            mis.IPaddress = "192.168.10.10";
            mis.numOfFirewall = 10;
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

        protected void abtAtkInfo_Command(object sender, CommandEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("testing:" +e.CommandArgument.ToString());
            AttackTypeHeader.Text = e.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "attackTypeModel", "showPopupattackinfo();", true);

        }
    }
}