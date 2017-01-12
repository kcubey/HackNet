using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Security
{
	public class Messaging
	{
		public static List<Message> RetrieveMessages(int Sender, int Receiver, int Viewer, byte[] aesKey)
		{
			using (DataContext db = new DataContext())
			{
				string SenderEmail, ReceiverEmail;
				UserKeyStore SenderUKS, ReceiverUKS;
				ReceiverEmail = db.Users.Find(Receiver).Email;
				SenderEmail = db.Users.Find(Sender).Email;
				using (Authenticate a = new Authenticate(ReceiverEmail))
					{ ReceiverUKS = a.GetKeyStore(db); }
				using (Authenticate a = new Authenticate(SenderEmail))
					{ SenderUKS = a.GetKeyStore(db); }
				List<Messages> msgs = db.Messages
			}
			return null;
		}

		public static void SendMessage(Message msg)
		{
			using (DataContext db = new DataContext())
			{
				string SenderEmail, ReceiverEmail;
				UserKeyStore SenderUKS, ReceiverUKS;
				SenderEmail = db.Users.Find(msg.SenderId).Email;
				ReceiverEmail = db.Users.Find(msg.RecipientId).Email;

				using (Authenticate a = new Authenticate(SenderEmail))
					SenderUKS = a.GetKeyStore(db);

				using (Authenticate a = new Authenticate(ReceiverEmail))
					ReceiverUKS = a.GetKeyStore(db);

				Messages dbMsg = msg.ToDatabase(ReceiverUKS.RsaPub, SenderUKS.RsaPub);

				db.Messages.Add(dbMsg);
			}
		}
	}
}