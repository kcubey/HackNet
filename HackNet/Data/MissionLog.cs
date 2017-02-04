using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class MissionLog
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LogId { get; set; }

		public int UserId { get; set; }

		public string MissionName { get; set; }

		public bool Successful { get; set; }

		public DateTime Timestamp { get; set; }

		public string Rewards { get; set; } // Stored as JSON
	}
}