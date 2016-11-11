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
    }
}