using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public partial class Messages
	{

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MsgId { get; set; }

		[ForeignKey("Sender")]
		public int SenderId { get; set; }

		public byte[] SenderEncrypted { get; set; }

		[ForeignKey("Receiver")]
		public int ReceiverId { get; set; }

		public byte[] ReceiverEncrypted { get; set; }

		public DateTime Timestamp { get; set; }
		
		// Foreign key reference
		public virtual Users Sender { get; set; }
		public virtual Users Receiver { get; set; }
	}
}