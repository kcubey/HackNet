using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public partial class Conversation
	{

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ConId { get; set; }

		[ForeignKey("UserA")]
		public int UserAId { get; set; }

		public byte[] KeyA { get; set; }

		public bool UnreadForA { get; set; }

		[ForeignKey("UserB")]
		public int UserBId { get; set; }

		public byte[] KeyB { get; set; }

		public bool UnreadForB { get; set; }
		
		// Foreign key reference
		public virtual Users UserA { get; set; }
		public virtual Users UserB { get; set; }
	}
}