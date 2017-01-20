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

		/// <summary>
		/// The encrypted AES key that can only be decrypted by A's private key
		/// </summary>
		public byte[] KeyA { get; set; }

		/// <summary>
		/// Whether A has any unread pending messages
		/// </summary>
		public bool UnreadForA { get; set; }

		[ForeignKey("UserB")]
		public int UserBId { get; set; }

		/// <summary>
		/// The encrypted AES key that can only be decrypted by B's private key
		/// </summary>
		public byte[] KeyB { get; set; }

		/// <summary>
		/// Whether B has any pending unread messages
		/// </summary>
		public bool UnreadForB { get; set; }
		
		// Foreign key reference
		public virtual Users UserA { get; set; }
		public virtual Users UserB { get; set; }
	}
}