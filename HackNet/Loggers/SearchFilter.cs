using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Loggers
{
	public class SearchFilter
	{
		public LogType Type { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }

		public int TypeInt
		{
			get
			{
				return (int)Type;
			}

			set
			{
				Type = (LogType)value;
			}
		}
	}
}