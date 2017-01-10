using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Loggers
{
	[Serializable]
	internal class LogEntryInvalidException : Exception
	{
		internal LogEntryInvalidException() : base() { }
		internal LogEntryInvalidException(string str) : base(str) { }
		internal LogEntryInvalidException(string str, Exception inner) : base(str, inner) { }
	}

	[Serializable]
	internal class LogTypeInvalidException : Exception
	{
		internal LogTypeInvalidException() : base() { }
		internal LogTypeInvalidException(string str) : base(str) { }
		internal LogTypeInvalidException(string str, Exception inner) : base(str, inner) { }
	}
}