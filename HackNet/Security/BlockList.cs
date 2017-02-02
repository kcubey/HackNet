using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Security
{
	public class BlockList
	{
		private static Dictionary<string, int> attempts = new Dictionary<string, int>();

		public static void AddOrUpdateAttempt(string ip)
		{
			if (attempts.ContainsKey(ip))
			{
				attempts[ip]++;
			}
			else
			{
				attempts.Add(ip, 1);
			}
		}

		public static bool CheckBlocked(string ip)
		{
			if (attempts.ContainsKey(ip))
			{
				if (attempts[ip] > 5)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}
		}
	}
}