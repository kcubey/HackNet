using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HackNet.Loggers
{
	public class ProfileLogger : Logger
	{
		private static ProfileLogger _inst;
		private ProfileLogger() { }

		public static ProfileLogger Instance
		{
			get
			{
				if (_inst == null)
					_inst = new ProfileLogger();
				return _inst;
			}
		}


		internal void ProfileChange(string email, int userid, string property, string old, string newval)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				UserId = userid,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = string.Format("Profile change ({0}) was made: '{1}' to '{2}'", property, old, newval)
			};
			Log(entry);
		}

		internal override void Log(LogEntry entry)
		{
			entry.Type = LogType.Profile;
			MasterLog(entry);
		}

		internal override List<LogEntry> Retrieve(SearchFilter sf)
		{
			List<LogEntry> results = new List<LogEntry>();
			using (DataContext db = new DataContext()) {
				List<Logs> logs = (from log in db.Logs
								   where log.Type == sf.TypeInt 
								   && DateTime.Compare(sf.End, log.Timestamp) >= 0
								   && DateTime.Compare(sf.Start, log.Timestamp) <= 0
								   select log).ToList();
				foreach (Logs l in logs) {
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