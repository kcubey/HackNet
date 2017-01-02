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

		internal void MissionCompleted(string missionname, string rewards)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "Mission completed: " + missionname + ", Rewards: " + rewards
			};
			Log(entry);
		}

		internal void ItemPurchased(string purchaseditem, string cost)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
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
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = string.Format("Item was sold to market: {0} for {1}", solditem, cost)
			};
			Log(entry);
		}

		internal override void Log(LogEntry entry)
		{
			entry.Type = LogType.Game;
			Thread t = new Thread(delegate ()
			{
				LogToDB(entry);
				LogToFile(entry);
				LogConsole(entry);
			}
			);
			t.Start();
		}

		internal override List<LogEntry> Retrieve(int UserId, DateTime? start, DateTime? end)
		{
			return null;
		}
	}
}