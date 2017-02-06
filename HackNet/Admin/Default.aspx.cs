using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Admin
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            using(DataContext db=new DataContext())
            {
                TotalNumUserLbl.Text=db.Users.Count().ToString();
                TotalNumMissionLbl.Text=db.MissionData.Count().ToString();
                TotalNumPlayedLbl.Text = db.MissionLog.Count().ToString();
                TotalItemLbl.Text=db.Items.Count().ToString();
            }
		}
	}
}