using HackNet.Data;
using HackNet.Security;
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


		internal void PasswordFail(string email, int userid)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				UserId = userid,
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "Password login failed for user"
			};
			Log(entry);
		}

		internal void PasswordSuccess(string email, int uid)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				UserId = uid,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "Password login successful for user"
			};
			Log(entry);
		}

		internal void PasswordChanged()
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "Password was changed by user"
			};
			Log(entry);
		}

		internal void TOTPFail()
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "One-time password authentication failed"
			};
			Log(entry);
		}

		internal void TOTPSuccess()
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "One-time password authentication successful"
			};
			Log(entry);
		}

		internal void TOTPChanged()
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "One-time password authentication reconfigured by user"
			};
			Log(entry);
		}

		internal void TOTPDisabled()
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "One-time password authentication disabled by user"
			};
			Log(entry);
		}

		internal void UserRegistered()
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = Authenticate.GetEmail(),
				UserId = Authenticate.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "New user has been registered"
			};
			Log(entry);
		}

		internal void UserNotFound(string email)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = null,
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "User was not found when authenticating " + email
			};
			Log(entry);
		}

		internal override void Log(LogEntry entry)
		{
			entry.Type = LogType.Security;
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
			List<LogEntry> results = new List<LogEntry>();
			using (DataContext db = new DataContext()) {
				int logtype = (int)LogType.Security;
				List<Logs> logs = (from log in db.Logs
								   where log.Type == logtype
								   select log).ToList();
				foreach (Logs l in logs)
					results.Add(LogEntry.ConvertFromDB(l));

			}

			return results;
		}
	}
}