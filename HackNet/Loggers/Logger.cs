using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Loggers
{
	public class Logger
	{
		private static Logger _instance;

		private Logger()
		{

		}

		internal Logger Instance
		{
			get
			{
				if (_instance == null)
					_instance = new Logger();
				return _instance;
			}
		}

	}
}