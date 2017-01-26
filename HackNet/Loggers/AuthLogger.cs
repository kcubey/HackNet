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
			if (Global.IsInUnitTest)
				return;

			LogEntry entry = new LogEntry()
			{
				EmailAddress = CurrentUser.GetEmail(),
				UserId = CurrentUser.GetUserId(),
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
				EmailAddress = CurrentUser.GetEmail(),
				UserId = CurrentUser.GetUserId(),
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
				EmailAddress = CurrentUser.GetEmail(),
				UserId = CurrentUser.GetUserId(),
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
				EmailAddress = CurrentUser.GetEmail(),
				UserId = CurrentUser.GetUserId(),
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
				EmailAddress = CurrentUser.GetEmail(),
				UserId = CurrentUser.GetUserId(),
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "One-time password authentication disabled by user"
			};
			Log(entry);
		}

		internal void UserRegistered(string email, int userId)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				UserId = userId,
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