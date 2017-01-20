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
		public int ConversationId { get; private set; }

		public byte[] AesKeyBytes { get; private set; } // Only if destined for database
		public string Content { get; private set; }

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
		internal Message(int SenderId, string Content)
		{
			this.SenderId = SenderId;
			this.Content = Content;
			Timestamp = DateTime.Now;
		}

		internal Message(int SenderId, string Content, DateTime Timestamp)
		{
			this.SenderId = SenderId;
			this.Content = Content;
			this.Timestamp = Timestamp;
		}

		internal SecureMessage ToDatabase(int recipientId)
		{
			byte[] contentBytes = Encoding.UTF8.GetBytes(Content);
			byte[] generatedIv = Crypt.Instance.GenerateIv("AES");

			SecureMessage dbMsg = new SecureMessage()
			{
				SenderId = SenderId,
				Message = Crypt.Instance.EncryptAes(contentBytes, AesKeyBytes, generatedIv),
				Timestamp = Timestamp,
				EncryptionIV = generatedIv,
			};
			return dbMsg;
		}
	}
}