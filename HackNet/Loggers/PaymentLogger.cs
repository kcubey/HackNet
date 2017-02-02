using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace HackNet.Loggers
{
	public class PaymentLogger : Logger
	{
		private static PaymentLogger _inst;
		private PaymentLogger() { }

		public static PaymentLogger Instance
		{
			get
			{
				if (_inst == null)
					_inst = new PaymentLogger();
				return _inst;
			}
		}


		internal void PaymentSuccess(string email, int userid)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				UserId = userid,
				IPAddress = GetIP(),
				Severity = LogSeverity.WARN,
				Description = "Payment has been made successfully"
			};
			Log(entry);
		}

		internal void PaymentFailed(string email, int userid)
		{
			LogEntry entry = new LogEntry()
			{
				EmailAddress = email,
				UserId = userid,
				IPAddress = GetIP(),
				Severity = LogSeverity.INFO,
				Description = "Payment attempt was unsuccessful"
			};
			Log(entry);
		}

		internal override void Log(LogEntry entry)
		{
			entry.Type = LogType.Payment;
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