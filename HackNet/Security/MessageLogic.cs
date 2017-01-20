using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace HackNet.Security
{
	public static class MessageLogic
	{
		public static ICollection<Message> RetrieveMessages(int Sender, int Receiver, int Viewer, KeyStore ks, int amount = -1)
		{
			List<Message> decryptedMessages = new List<Message>();
			List<SecureMessage> dbMsgList;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			using (DataContext db = new DataContext())
			{
				// DB Query for messages that match query
				var dbMsgQueryable = db.SecureMessage.Where(m =>
						(m.SenderId == Sender || m.SenderId == Receiver)).OrderByDescending(m => m.MsgId);

				int rows = dbMsgQueryable.Count();

				// If the number of rows exceeds the number of rows, return all the rows
				if (amount > rows)
					amount = -1;

				// Get the specified number of rows from the database
				if (amount > 0)
					dbMsgList = dbMsgQueryable.Take(amount).ToList();
				else
					dbMsgList = dbMsgQueryable.ToList();
				sw.Stop();
				Debug.WriteLine("Messages retrieval took: " + sw.ElapsedMilliseconds + "ms");

				// Convert each message into decrypted form
				sw.Reset();
				sw.Start();
				foreach (SecureMessage dbMsg in dbMsgList)
				{
					// TODO: decryptedMessages.Add(new Message(dbMsg, Viewer, new ));
				}
				sw.Stop();
				Debug.WriteLine("Messages decryption took: " + sw.ElapsedMilliseconds + "ms");
			}
			return decryptedMessages;
		}

		public static void SendMessage(Message msg)
		{
			using (DataContext db = new DataContext())
			{
				string sPubKey;

				// Get the email address of concerned users
				string sEmail = db.Users.Find(msg.SenderId).Email;

				// Get the UserKeyStore object of the users
				using (Authenticate a = new Authenticate())
				{
					//sPubKey = a.GetRsaPublic(db, sEmail);
				}

				// Convert the message to database format while encrypting it
				SecureMessage dbMsg = msg.ToDatabase(new byte[0]);

				// Add to database
				db.SecureMessage.Add(dbMsg);
				db.SaveChanges();
			}
		}

		public static ICollection<string> RetrieveRecents(int viewerId)
		{
			List<string> recents = new List<string>();
			List<int> recentIds = new List<int>();

			if (viewerId <= 0)
				return recents;

			using (DataContext db = new DataContext())
			{
				// TODO: new implementation

				recentIds.Remove(viewerId);

				foreach (int id in recentIds)
				{
					Users u = db.Users.Find(id);
					if (u != null)
						recents.Add(u.UserName);
				}

				return recents;
			}
		}


		/// <summary>
		/// Creates a conversation between 2 parties if it does not exist.
		/// </summary>
		/// <param name="PartyA">User ID of Party A</param>
		/// <param name="PartyB">User ID of Party B</param>
		/// <returns></returns>
		public static int GetConversationId(int PartyA, int PartyB)
		{
			using (DataContext db = new DataContext())
			{
				Conversation conv = db.Conversation.Where(c =>
										(c.UserAId == PartyA && c.UserBId == PartyB) ||
										(c.UserBId == PartyA && c.UserAId == PartyB)).FirstOrDefault();
				if (conv == null)
					using (Authenticate a = new Authenticate())
					{
						byte[] symmAesKey = Crypt.Instance.GenerateAesKey();
						string rsaA = a.GetRsaPublic(db, PartyA);
						string rsaB = a.GetRsaPublic(db, PartyB);
						byte[] encryptedAesKeyA = Crypt.Instance.EncryptRsa(symmAesKey, rsaA);
						byte[] encryptedAesKeyB = Crypt.Instance.EncryptRsa(symmAesKey, rsaB);
						Conversation newConv = new Conversation()
						{
							KeyA = encryptedAesKeyA,
							KeyB = encryptedAesKeyB,
							UnreadForA = false,
							UnreadForB = false,
							UserAId = PartyA,
							UserBId = PartyB
						};

						db.Conversation.Add(conv);
						db.SaveChanges();

						conv = db.Conversation.Where(c => (c.UserAId == PartyA && c.UserBId == PartyB)).FirstOrDefault();

					}

				if (conv == null)
					throw new ChatException("Conversation could not be loaded.");

				return conv.ConId;

			}
		}
	}
}
