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


		internal void PasswordFail(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "Password login failed for user"
			};
			Log(entry);
		}

		internal void PasswordSuccess(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "Password login successful for user"
			};
			Log(entry);
		}

		internal void PasswordChanged(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "Password was changed by user"
			};
			Log(entry);
		}

		internal void TOTPFail(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "One-time password authentication failed"
			};
			Log(entry);
		}

		internal void TOTPSuccess(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "One-time password authentication successful"
			};
			Log(entry);
		}

		internal void TOTPChanged(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "One-time password authentication reconfigured by user"
			};
			Log(entry);
		}

		internal void TOTPDisabled(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "One-time password authentication disabled by user"
			};
			Log(entry);
		}

		internal void UserRegistered(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "New user has been registered"
			};
			Log(entry);
		}

		internal override void Log(LogEntry entry)
		{
			Thread t = new Thread(delegate () 
				{
					LogToDB(entry);
					LogToFile(entry);
					LogConsole(entry);
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