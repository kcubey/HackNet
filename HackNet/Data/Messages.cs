using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
	public class Messages
	{
		public int MsgId { get; set; }

		public int SenderId { get; set; }

		public byte[] SenderEncryoted { get; set; }

		public int ReceiverId { get; set; }

		public byte[] ReceiverEncrypted { get; set; }

		public DateTime Timestamp { get; set; }
	}
}