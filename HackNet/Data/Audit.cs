using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class Audit
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string LogId { get; set; }

		[Required]
		public AuditType Type { get; set;}

		public DateTime Timestamp { get; set; }

		public string IpAddress { get; set; }

		[Required]
		public string Description { get; set; }

		[ForeignKey("User")]
		public int UserId { get; set; }

		public Users User { get; set; }
    }

	public enum AuditType
	{
		Normal = 0,
		AuthFailure = 1,
		Validation = 2,
		SysError = 3
	}
}