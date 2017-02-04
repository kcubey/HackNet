using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace HackNet.Loggers
{
	public static class MissionLogLogic
	{
		public static void Store(int userid, string missionName, bool result, List<string> Rewards = null)
		{
			JsonSerializer js = new JsonSerializer();

			if (Rewards == null)
				Rewards = new List<string>();

			using (DataContext db = new DataContext())
			{
				MissionLog ml = new MissionLog()
				{
					MissionName = missionName,
					Rewards = JsonConvert.SerializeObject(Rewards),
					Timestamp = DateTime.Now,
					Successful = result,
					UserId = userid
				};

				db.MissionLog.Add(ml);
				db.SaveChanges();
			}
		}
	}
}