using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Security
{
	public class RateLimit
	{
        public static RateLimit Instance
        {
            get
            {
                if (_inst == null)
                    _inst = new RateLimit();

                return _inst;
            }
        }

        private static RateLimit _inst;

        private RateLimit() { }

		private Dictionary<string, int> attempts = new Dictionary<string, int>();

		public void AddOrUpdateAttempt(string ip)
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

		public int GetDelay(string ip)
		{
			if (attempts.ContainsKey(ip))
			{
                int secs = Convert.ToInt32((attempts[ip] - 1) * 5);
                System.Diagnostics.Debug.WriteLine("Delaying for: " + secs);
                return secs;
			}
			else
			{
				return 0;
			}
		}
	}
}