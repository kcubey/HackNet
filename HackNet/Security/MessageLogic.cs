using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HackNet.Security
{
	public class MessageLogic
	{
		public static List<Message> RetrieveMessages(int Sender, int Receiver, int Viewer, KeyStore ks)
		{
			List<Message> decryptedMessages = new List<Message>();
			using (DataContext db = new DataContext())
			{
				// DB Query for messages that match query
				List<Messages> dbMessages = 
					db.Messages.Where(m => m.SenderId == Sender && m.ReceiverId == Receiver).ToList();

				// Convert each message into decrypted form
				foreach(Messages dbMsg in dbMessages)
				{
					decryptedMessages.Add(new Message(dbMsg, Viewer, ks.rsaPrivate));
				}
			}
			return decryptedMessages;
		}

		public static void SendMessage(Message msg)
		{
			using (DataContext db = new DataContext())
			{
				string sPubKey, rPubKey;

				// Get the email address of concerned users
				string sEmail = db.Users.Find(msg.SenderId).Email;
				string rEmail = db.Users.Find(msg.RecipientId).Email;

				// Get the UserKeyStore object of the users
				using (Authenticate a = new Authenticate()) {
					sPubKey = a.GetRsaPublic(sEmail);
					rPubKey = a.GetRsaPublic(rEmail);
				}
				
				// Convert the message to database format while encrypting it
				Messages dbMsg = msg.ToDatabase(rPubKey, sPubKey);

				// Add to database
				db.Messages.Add(dbMsg);
				db.SaveChanges();
			}
		}
	}
}