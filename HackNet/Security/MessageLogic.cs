using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HackNet.Security
{
	public static class MessageLogic
	{
		public static ICollection<Message> RetrieveMessages(int retriever, int other, KeyStore ks, int amount = -1)
		{
			ICollection<Message> decryptedMessages = new List<Message>();
			List<SecureMessage> dbMsgList = null;
			byte[] convAesKey = null;

			Stopwatch sw = new Stopwatch();                                        
			sw.Start();
			using (DataContext db = new DataContext())
			{
				var dbConv = GetConversation(retriever, other, db);

				if (dbConv == null)
					throw new ChatException("Message could not be retrieved");

				if (retriever == dbConv.UserAId)
				{
					convAesKey = Crypt.Instance.DecryptRsa(dbConv.KeyA, ks.rsaPrivate);
					if (dbConv.UnreadForA == true)
					{
						dbConv.UnreadForA = false;
						db.SaveChanges();
					}
				}
				else if (retriever == dbConv.UserBId)
				{
					convAesKey = Crypt.Instance.DecryptRsa(dbConv.KeyB, ks.rsaPrivate);
					if (dbConv.UnreadForB == true)
					{
						dbConv.UnreadForB = false;
						db.SaveChanges();
					}
				}

				if (convAesKey == null)
					throw new ChatException("Message could not be decrypted (AES from RSA)");

				// DB Query for messages that match query
				var dbMsgQueryable = db.SecureMessage
										.Where(m => (m.ConId == dbConv.ConId))
										.OrderByDescending(m => m.MsgId);

				int rows = dbMsgQueryable.Count();

				// If the number of rows exceeds the number of rows, return all the rows
				if (amount > rows)
					amount = -1;

				// Get the specified number of rows from the database
				if (amount > 0)
					dbMsgList = dbMsgQueryable.Take(amount).ToList();
				else
					dbMsgList = dbMsgQueryable.ToList();

				// Convert each message into decrypted form
				foreach (SecureMessage dbMsg in dbMsgList)
				{
					decryptedMessages.Add(new Message(dbMsg, convAesKey));
				}

				sw.Stop();
				Debug.WriteLine("Messages retrieval & decryption took: " + sw.ElapsedMilliseconds + "ms");
			}
			return decryptedMessages.Reverse().ToList();
		}

		public static void SendMessage(Message msg, KeyStore ks)
		{
			byte[] convAesKey = null;
			int recipientId = -1;

			using (DataContext db = new DataContext())
			{
				var conv = GetConversation(msg.SenderId, msg.RecipientId, db);
				msg.ConversationId = conv.ConId;

				// Non-blocking execution of setting unread
				Task.Run(() => SetUnread(conv.ConId, msg.RecipientId));

				if (msg.SenderId == conv.UserAId)
				{
					convAesKey = Crypt.Instance.DecryptRsa(conv.KeyA, ks.rsaPrivate);
					recipientId = conv.UserBId;
				}
				else if (msg.SenderId == conv.UserBId)
				{
					convAesKey = Crypt.Instance.DecryptRsa(conv.KeyB, ks.rsaPrivate);
					recipientId = conv.UserAId;
				}

				SecureMessage dbMsg = msg.ToDatabase(recipientId, convAesKey);

				if (convAesKey == null || recipientId == -1)
					throw new ChatException("Recipient invalid or message could not be encrypted");

				// Add to database
				db.SecureMessage.Add(dbMsg);
				db.SaveChanges();
			}
		}

		public static void SetUnread(int convId, int unreadFor) // Should be run non-blocking
		{
			using (DataContext db = new DataContext())
			{
				Conversation c = db.Conversation.Find(convId);

				if (c.UserAId == unreadFor)
					c.UnreadForA = true;
				else if (c.UserBId == unreadFor)
					c.UnreadForB = true;

				db.SaveChanges();
			}
		}

		public static IDictionary<string, bool> RetrieveRecents(int viewerId)
		{
			Dictionary<string, bool> recents = new Dictionary<string, bool>();
			Dictionary<int, bool> recentIds = new Dictionary<int, bool>();

			if (viewerId <= 0)
				return recents;

			using (DataContext db = new DataContext())
			{
				List<Conversation> convs = 
								db.Conversation.Where(c => 
								(c.UserAId == viewerId 
								|| c.UserBId == viewerId)).ToList();

				foreach(Conversation c in convs)
				{
					if (c.UserAId == viewerId)
						recentIds.Add(c.UserBId, c.UnreadForA);

					if (c.UserBId == viewerId)
						recentIds.Add(c.UserAId, c.UnreadForB);
				}

				foreach (var each in recentIds)
				{
					Users u = db.Users.Find(each.Key);
					if (u != null && each.Key != viewerId)
						recents.Add(u.UserName, each.Value);
				}

				return recents;
			}
		}


		/// <summary>
		/// Creates a conversation between 2 parties if it does not exist.
		/// </summary>
		/// <param name="PartyA">User ID of Party A</param>
		/// <param name="PartyB">User ID of Party B</param>
		/// <returns>Returns the existing/new conversation entity</returns>
		public static Conversation GetConversation(int PartyA, int PartyB, DataContext db)
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

					db.Conversation.Add(newConv);
					db.SaveChanges();

					conv = db.Conversation.Where(c => (c.UserAId == PartyA && c.UserBId == PartyB)).FirstOrDefault();

				}

			if (conv == null)
				throw new ChatException("Conversation could not be loaded.");

			return conv;
		}
		
	}
}
