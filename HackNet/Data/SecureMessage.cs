using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class SecureMessage
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int MsgId { get; set; }

		[ForeignKey("Conversation")]
		public int ConId { get; set; }

		[ForeignKey("Sender")]
		public int SenderId { get; set; }

		public DateTime Timestamp { get; set; }

		public byte[] Message { get; set; }

		public byte[] EncryptionIV { get; set; }

		// Foreign key references
		public Conversation Conversation { get; set; }

		public Users Sender { get; set; }

	}
}