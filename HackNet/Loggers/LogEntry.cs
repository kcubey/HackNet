using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HackNet.Data;

namespace HackNet.Loggers
{
	public class LogEntry
	{

		public int UserId { get; set; } // 0 if not attached to any user

		public DateTime Timestamp { get; set; }

		public LogType Type { get; set; }

		public LogSeverity Severity { get; set; }

		public string Description { get; set; } // Cannot be blank

		public string IPAddress { get; set; }

		public string EmailAddress { get; set; }

		public LogEntry()
		{
			Timestamp = DateTime.Now;
		}

		public bool IsValid
		{
			get
			{
				// Validates if entered data conforms to the standard
				if (UserId < 0)
					return false;
				if (Timestamp == null) // Timestamp cannot be null (C# doesnt allow that anyway)
					return false;
				if (string.IsNullOrWhiteSpace(Description)) // Description has to be filled
					return false;
				if (!Enum.IsDefined(typeof(LogSeverity), Severity)) // Enum has to be defined
					return false;
				if (!Enum.IsDefined(typeof(LogType), Type))
					return false;
				// After all guard clauses have been checked
				return true;
			}
		}

		public Logs ConvertToDB()
		{
			if (IsValid)
			{
				// Create EF object for insertion into DB
				Logs l = new Logs()
				{
					UserId = UserId,
					Type = (int)Type,
					Severity = (int)Severity,
					Description = Description,
					IPAddress = IPAddress,
					Timestamp = Timestamp
				};
				return l;
			} else
				throw new LogEntryInvalidException("Exception occured while converting to database type");
		}

		public static LogEntry ConvertFromDB(Logs dbl)
		{
			LogEntry l = new LogEntry()
			{
				UserId = dbl.UserId,
				Type = (LogType) dbl.Type,
				Severity = (LogSeverity) dbl.Severity,
				Description = dbl.Description,
				IPAddress = dbl.IPAddress,
				Timestamp = dbl.Timestamp
			};

			if (l.IsValid)
				return l;
			else
				throw new LogEntryInvalidException("Exception occured while converting from database type");
		}
	}

	public enum LogSeverity
	{
		DEBUG = 0,
		INFO = 1,
		WARN = 2,
		ERROR = 3,
		FATAL = 4
	}

	public enum LogType
	{
		Game = 0,
		Security = 1,
		Payment = 2,
		Profile = 3,
		Error = 4
	}
}