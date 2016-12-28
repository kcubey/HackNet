using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Loggers
{
	public class Logger
	{
		private static Logger _instance;
		private DataContext db;

		internal static Logger Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Logger();
				return _instance;
			}
		}

		private Logger()
		{
			db = new DataContext();
		}

		internal void Log(LogEntry entry)
		{

		}

		internal void LogAll(ICollection<LogEntry> entry)
		{

		}

		

	}
}