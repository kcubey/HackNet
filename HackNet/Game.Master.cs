using HackNet.Data;
using HackNet.Security;
using System;
using System.Web.UI;

namespace HackNet
{
    public partial class GameMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			Users u = Authenticate.GetCurrentUser();
			PlayerName.Text = u.UserName;
			CoinsLbl.Text = u.Coins.ToString();
			BucksLbl.Text = u.ByteDollars.ToString();
			int userCurrentLevel = u.Level.GetLevel();
			LevelsLbl.Text = userCurrentLevel.ToString();

			int expNeededForThisLvl = Level.TotalExpNeededFor(u.Level.GetLevel());
			int expNeededForNextLevel = u.Level.TotalForNextLevel();
			int expNeededInThisLevel = expNeededForNextLevel - expNeededForThisLvl;
			int expObtainedInThisLevel = u.TotalExp - expNeededForThisLvl;
			double percentageToNextLevel = ((double) u.TotalExp / expNeededForNextLevel) * 100;
			int intPctToNextLevel = Convert.ToInt32(percentageToNextLevel);
			string progressionStatement = string.Format(" {0} / {1} ({2} %)", u.TotalExp, expNeededForNextLevel, intPctToNextLevel);
			ExpProgressLbl.Text = progressionStatement;

			progressbar.Attributes["style"] = "width: " + intPctToNextLevel + "%";
			progressbar.Attributes["aria-valuenow"] = intPctToNextLevel.ToString(); ;


        }

        #region Navigation Links
        protected void LnkHome(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Home");
        }

        protected void LnkWsn(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Workstation");
        }

        protected void LnkMis(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Missions");
        }

        protected void LnkMLog(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Home");
        }

        protected void LnkInv(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Inventory");
        }

		protected void LnkChat(object sender, EventArgs e)
		{
			Response.Redirect("~/Game/Chat");
		}

        protected void LnkMkt(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Market");
        }

        protected void LnkMkt1(object sender, EventArgs e)
        {
            Response.Redirect("~/Game/Market1");
        }
        #endregion

    }
}