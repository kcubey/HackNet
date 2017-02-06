using HackNet.Data;
using HackNet.Loggers;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
	public partial class MissionLogs : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void MissionLog_Load(object sender, EventArgs e)
        {
            IList<MissionLog> misLog= MissionLogLogic.Get(CurrentUser.Entity().UserID);
            DataTable dt = new DataTable();
            dt.Columns.Add("Mission Name",typeof(string));
            dt.Columns.Add("Successful",typeof(bool));
            dt.Columns.Add("Time of Event",typeof(DateTime));
            dt.Columns.Add("Rewards", typeof(string));

            if (misLog.Count != 0)
            {
                foreach (var mlog in misLog)
                {
                    dt.Rows.Add(mlog.MissionName,mlog.Successful,mlog.Timestamp, ReturnReward(MissionLogLogic.DeserializeRewards(mlog.Rewards)));
                }
                MissionLog.DataSource = dt;
                MissionLog.DataBind();
            }
            else
            {
                MissionLog.DataSource = null;
                MissionLog.DataBind();
            }
        }

        private string ReturnReward(IList<string> rewardList)
        {
            string reward ="";
            foreach (string r in rewardList)
            {
                reward += r+ " ;";
            }
           
            return reward;
        }
    }
}