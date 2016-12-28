using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HackNet.Loggers
{
	public abstract class Logger
	{
		private AuthLogger Auth;
		private static DataContext db;

		public Logger()
		{
			if (db == null)
				db = new DataContext();
		}

		internal abstract void Log(LogEntry entry);

		internal abstract void LogAll(ICollection<LogEntry> entries);

		internal int LogToDB(LogEntry entry)
		{
			// Get related user
			Users u = Users.FindByEmail(entry.EmailAddress, db);

			if (u == null)
				entry.UserId = 0;
			else
				entry.UserId = u.UserID;

			Logs logForDb = new Logs()
			{
				UserId = entry.UserId,
				Type = (int)entry.Type,
				Severity = (int)entry.Severity,
				Description = entry.Description,
				IPAddress = entry.IPAddress,
				Timestamp = entry.Timestamp,
			};

			db.Logs.Add(logForDb);
			return db.SaveChanges();
		}

		internal void LogToFile(LogEntry entry)
		{
			
		}
	}
}