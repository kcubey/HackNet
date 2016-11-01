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
            ArrayList arrList = new ArrayList();
            arrList.Add("Port Scan: activated");
            arrList.Add("Scanning in progress");
            arrList.Add("10 open ports found");

            string username = "testuser"+"@HackNet:~#  ";
            for(int i=0;i<arrList.Count;i++)
            {
                Label lbl = new Label();
                lbl.Text = username+arrList[i].ToString();
                LogPanel.Controls.Add(lbl);
                LogPanel.Controls.Add(new LiteralControl("<br/>"));
            }
            
        }
    }
}