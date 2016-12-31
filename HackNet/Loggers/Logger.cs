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
			if (entry.UserId == 0 && entry.EmailAddress != null)
			{
				// Get related user
				Users u = Users.FindByEmail(entry.EmailAddress, db);
				// Check if user is null or does it actually exist
				if (u == null)
					entry.UserId = 0;
				else
					entry.UserId = u.UserID;
			}
			// Create EF object for insertion into DB
			Logs logForDb = new Logs()
			{
				UserId = entry.UserId,
				Type = (int)entry.Type,
				Severity = (int)entry.Severity,
				Description = entry.Description,
				IPAddress = entry.IPAddress,
				Timestamp = entry.Timestamp,
			};
			// Add and save changes
			db.Logs.Add(logForDb);
			return db.SaveChanges();
		}

		internal void LogToFile(LogEntry entry)
		{
			
		}

		internal void LogConsole(LogEntry entry)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				string severity = Enum.GetName(typeof(LogSeverity), entry.Severity);
				string type = Enum.GetName(typeof(LogType), entry.Type);
				string LogString = string.Format("[{0}] {1}: {2} by {3}", severity, type, entry.Description, entry.IPAddress);
				System.Diagnostics.Debug.WriteLine(LogString);
			}
		}


		internal string GetIP()
		{
			HttpContext context = HttpContext.Current;
			HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
			string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
			string userIp = "NIL";

			if (!string.IsNullOrEmpty(ipAddress))
			{
				string[] addresses = ipAddress.Split(',');
				if (addresses.Length != 0)
				{
					userIp = addresses[0];
				}
			}
			else
			{
				userIp = context.Request.ServerVariables["REMOTE_ADDR"];
			}

			if (userIp.Equals("::1"))
			{
				userIp = "127.0.0.1";
			}

			return browser.Type + " - " + userIp;
		}
	}
}