using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HackNet.Loggers
{
	public abstract class Logger
	{
		private static string path
		{
			get
			{
				if (Global.IsInUnitTest)
					return "";

				return HttpRuntime.AppDomainAppPath + "App_Data\\HackNet.log";
			}
		}

		internal abstract void Log(LogEntry le);

		internal abstract List<LogEntry> Retrieve(SearchFilter sf);

		internal void MasterLog(LogEntry entry)
		{
			Thread t = new Thread(delegate ()
			{
				LogToDB(entry);
				LogToFile(entry);
				LogConsole(entry);
			});
			t.Start();
		}

		internal static Logger GetRetriever(SearchFilter sf)
		{
			switch (sf.Type) {
				case LogType.Game:
					return GameLogger.Instance;
				case LogType.Security:
					return AuthLogger.Instance;
				case LogType.Payment:
					return PaymentLogger.Instance;
				case LogType.Profile:
					return ProfileLogger.Instance;
				case LogType.Error:
				default:
					return null;
			}
		}

		internal int LogToDB(LogEntry entry)
		{
			using (DataContext db = new DataContext())
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

				// Convert to EF supported type
				Logs dblog = null;
				if (entry.IsValid)
					dblog = entry.ConvertToDB();

				// Add and save changes
				if (dblog != null)
					db.Logs.Add(dblog);

				return db.SaveChanges();
			}
		}

		internal void LogToFile(LogEntry entry)
		{
			if (Global.IsInUnitTest)
				return;

			string severity = Enum.GetName(typeof(LogSeverity), entry.Severity);
			string type = Enum.GetName(typeof(LogType), entry.Type);
			string time = DateTime.Now.ToString();
			
			using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
			using (StreamWriter sw = new StreamWriter(fs))
			{
				try
				{

					string LogString = string.Format("[{0} {1}] {2}: {3} by {4}",
										severity, time, type, entry.Description, entry.IPAddress);

					sw.WriteLine(LogString);
					sw.Flush();
				} catch (UnauthorizedAccessException)
				{
					System.Diagnostics.Debug.WriteLine("File logging failed with exception (UnauthorizedAccessException)");
				} catch (SecurityException)
				{
					System.Diagnostics.Debug.WriteLine("File logging failed with exception (SecurityException)");
				} finally
				{
					sw.Close();
				}
			}
		}

		internal static string GetLogFile()
		{
			string allLines;
			using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read))
			using (StreamReader sr = new StreamReader(fs))
			{
				allLines = sr.ReadToEnd();
			}
			return allLines;
		}

		internal void LogConsole(LogEntry entry)
		{
			if (System.Diagnostics.Debugger.IsAttached)
			{
				string severity = Enum.GetName(typeof(LogSeverity), entry.Severity);
				string type = Enum.GetName(typeof(LogType), entry.Type);
				string LogString = string.Format("[{0}] {1}: {2} on {3} by {4}", severity, type, entry.Description, entry.EmailAddress, entry.IPAddress);
				System.Diagnostics.Debug.WriteLine(LogString);
			}
		}


		internal string GetIP()
		{
			if (Global.IsInUnitTest)
				return "Test Environment";

			if (HttpContext.Current == null)
				throw new SecurityException("HttpContext not found while executing.");

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