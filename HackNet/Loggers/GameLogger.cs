using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HackNet.Loggers
{
	public class GameLogger : Logger
	{
		private static GameLogger _inst;
		private GameLogger() { }

		public static GameLogger Instance
		{
			get
			{
				if (_inst == null)
					_inst = new GameLogger();
				return _inst;
			}
		}

        internal void MissionSuccess(string missionname, string rewards)
        {
            LogEntry entry = new LogEntry()
            {
                EmailAddress = CurrentUser.GetEmail(),
                UserId = CurrentUser.GetUserId(),
                IPAddress = GetIP(),
                Severity = LogSeverity.INFO,
                Description = "Mission completed: " + missionname + ", Rewards: " + rewards
            };
            Log(entry);
        }

        internal void MissionFail(string missionname)
        {
            LogEntry entry = new LogEntry()
            {
                EmailAddress = CurrentUser.GetEmail(),
                UserId = CurrentUser.GetUserId(),
                IPAddress = GetIP(),
                Severity = LogSeverity.INFO,
                Description = "Mission failed: " + missionname
            };
            Log(entry);
        }

        internal void ItemPurchased(string purchaseditem, string cost)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = CurrentUser.GetEmail(),
				UserId = CurrentUser.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = string.Format("Item was purchased from market: {0} for {1}", purchaseditem, cost)
			};
			Log(entry);
		}

		internal void ItemSold(string solditem, string cost)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = CurrentUser.GetEmail(),
				UserId = CurrentUser.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = string.Format("Item was sold to market: {0} for {1}", solditem, cost)
			};
			Log(entry);
		}

		internal override void Log(LogEntry entry)
		{
			entry.Type = LogType.Game;
			MasterLog(entry);
		}

		internal override List<LogEntry> Retrieve(SearchFilter sf)
		{
			List<LogEntry> results = new List<LogEntry>();
			using (DataContext db = new DataContext())
			{
				List<Logs> logs = (from log in db.Logs
								   where log.Type == sf.TypeInt
								   && DateTime.Compare(sf.End, log.Timestamp) >= 0
								   && DateTime.Compare(sf.Start, log.Timestamp) <= 0
								   select log).ToList();
				foreach (Logs l in logs)
				{
					LogEntry entry = LogEntry.ConvertFromDB(l);
					if (sf.UserId == entry.UserId || sf.UserId == -1)
					{
						results.Add(entry);
					}
				}
			}
			return results;
		}
	}
}