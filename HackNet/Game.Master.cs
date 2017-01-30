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
			if (!IsPostBack)
			{
				Users u = CurrentUser.Entity();
				PlayerName.Text = u.UserName;
				CoinsLbl.Text = u.Coins.ToString();
				BucksLbl.Text = u.ByteDollars.ToString();
				int userCurrentLevel = u.Level.GetLevel();
				LevelsLbl.Text = userCurrentLevel.ToString();

				int expNeededForThisLvl = Level.TotalExpNeededFor(u.Level.GetLevel());
				int expNeededForNextLevel = u.Level.TotalForNextLevel();
				int expNeededInThisLevel = expNeededForNextLevel - expNeededForThisLvl;
				int expObtainedInThisLevel = u.TotalExp - expNeededForThisLvl;
				double percentageToNextLevel = ((double)expObtainedInThisLevel / expNeededInThisLevel) * 100;
				int intPctToNextLevel = Convert.ToInt32(Math.Floor(percentageToNextLevel));

				string progressionStatement = string.Format(" {0} / {1} ({2} %)",
														expObtainedInThisLevel,
														expNeededInThisLevel,
														intPctToNextLevel);

				ExpProgressLbl.Text = progressionStatement;
				TotalProgressLbl.Text = "Total Exp: " + u.TotalExp;
				progressbar.Attributes["style"] = "width: " + intPctToNextLevel + "%";
				progressbar.Attributes["aria-valuenow"] = intPctToNextLevel.ToString();
			}


        }

    }
}