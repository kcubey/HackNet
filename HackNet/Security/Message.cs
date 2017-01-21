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
		public int MessageId { get; private set; } // Only if destined from database

		public int SenderId { get; private set; }

		public int RecipientId { get; private set; }

		public int ConversationId { get; internal set; }

		public string Content { get; internal set; }

		public DateTime Timestamp { get; private set; }


		/// <summary>
		/// Created Message with information from database
		/// </summary>
		/// <param name="dbMsg"></param>
		/// <param name="aesKey"></param>
		internal Message(SecureMessage dbMsg, byte[] aesKey)
		{
			byte[] msgEncrypted = dbMsg.Message;
			byte[] msgIv = dbMsg.EncryptionIV;
			byte[] msgBytes = Crypt.Instance.DecryptAes(dbMsg.Message, aesKey, msgIv);
			string msgString = Encoding.UTF8.GetString(msgBytes);

			SenderId = dbMsg.SenderId;
			Content = msgString;
			Timestamp = dbMsg.Timestamp;
			ConversationId = dbMsg.ConId;
		}

		// Message creation
		internal Message(int senderId, int convId, string msgContent)
		{
			SenderId = senderId;
			RecipientId = convId;
			Content = msgContent;
			Timestamp = DateTime.Now;

		}

		internal SecureMessage ToDatabase(int recipientId, byte[] aesKeyBytes)
		{
			byte[] contentBytes = Encoding.UTF8.GetBytes(Content);
			byte[] generatedIv = Crypt.Instance.GenerateIv("AES");

			SecureMessage dbMsg = new SecureMessage()
			{
				SenderId = SenderId,
				Message = Crypt.Instance.EncryptAes(contentBytes, aesKeyBytes, generatedIv),
				ConId = ConversationId,
				Timestamp = Timestamp,
				EncryptionIV = generatedIv,
			};
			return dbMsg;
		}
	}
}