using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HackNet.Loggers
{
	public class AuthLogger : Logger
	{
		private static AuthLogger _inst;
		private AuthLogger() { }

		public static AuthLogger Instance
		{
			get
			{
				if (_inst == null)
					_inst = new AuthLogger();
				return _inst;
			}
		}


		internal void FailedLogin(string email, string ip)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = ip,
				Severity = LogSeverity.WARN,
				Description = "Password login failed for user " + email
			};
			Log(entry);
		}

		internal override void Log(LogEntry entry)
		{
			Thread t = new Thread(delegate () 
				{
					LogToDB(entry);
					LogToFile(entry);
				}
			);
			t.Start();
		}

		internal override void LogAll(ICollection<LogEntry> entries)
		{
			foreach(LogEntry e in entries)
			{
				LogToDB(e);
				LogToFile(e);
			}
		}
	}
}