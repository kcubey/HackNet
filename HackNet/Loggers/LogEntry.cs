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

		public string IpAddress { get; set; }

		public bool IsValid()
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

			return true;
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
		Database = 3
	}
}