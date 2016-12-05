using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class MissionLogs
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LogId { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }

		[ForeignKey("Mission")]
		public int MissionId { get; set; }

		public string MisisonIp { get; set; }

		public Users User { get; set; }

		public MissionData Mission { get; set; }
	}
}