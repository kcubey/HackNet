using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HackNet.Security
{
	public class Message
	{
		public int SenderId { get; private set; }
		public int RecipientId { get; private set; }
		public string Content { get; private set; }
		public DateTime Timestamp { get; private set; }
		public byte[] SHA1Sum { get; private set; }

		public int MessageId  { get; private set; } // Only if made from constructor

		// Convert to database type
		internal Messages ToDatabase(string RPubKey, string SPubKey)
		{
			byte[] contentBytes = Encoding.UTF8.GetBytes(Content);
			Messages dbMsg = new Messages()
			{
				SenderId = SenderId,
				ReceiverId = RecipientId,
				ReceiverEncrypted = Crypt.Instance.EncryptRsa(contentBytes, RPubKey),
				SenderEncrypted = Crypt.Instance.EncryptRsa(contentBytes, SPubKey),
				Timestamp = Timestamp
			};
			return dbMsg;
		}

		internal Message(Messages dbMsg, int ViewerId, string PrivKey)
		{
			byte[] msgBytes;
			string msgString;

			// Identify which one from DB to decrypt
			if (dbMsg.SenderId == ViewerId) // When viewer is SENDER
				msgBytes = Crypt.Instance.DecryptRsa(dbMsg.SenderEncrypted, PrivKey);
			else if (dbMsg.ReceiverId == ViewerId) // When viewer is RECEIVER
				msgBytes = Crypt.Instance.DecryptRsa(dbMsg.ReceiverEncrypted, PrivKey);
			else // Something isnt right, maybe an exception should be thrown
				msgBytes = new byte[0];

			// Convert bytes to string
			if (msgBytes.Length != 0 && msgBytes != null)
				msgString = Encoding.UTF8.GetString(msgBytes);
			else
				msgString = "Message content could not be shown.";

			SenderId = dbMsg.SenderId;
			RecipientId = dbMsg.ReceiverId;
			Content = msgString;
			Timestamp = dbMsg.Timestamp;
		}

		// Message creation
		internal Message(int SenderId, int RecipientId, string Content)
		{
			this.SenderId = SenderId;
			this.RecipientId = RecipientId;
			this.Content = Content;
			Timestamp = DateTime.Now;
			SHA1Sum = Crypt.Instance.Hash(Encoding.UTF8.GetBytes(Content));
		}

		internal Message(int SenderId, int RecipientId, string Content, DateTime Timestamp)
		{
			this.SenderId = SenderId;
			this.RecipientId = RecipientId;
			this.Content = Content;
			this.Timestamp = Timestamp;
			SHA1Sum = Crypt.Instance.Hash(Encoding.UTF8.GetBytes(Content));
		}

	}
}