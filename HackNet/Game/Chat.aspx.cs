using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
	public partial class Chat : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["KeyStore"] == null)
				Response.Redirect("~/Default", true);

			
			Message msg = new Message(Authenticate.GetUserId(), 1, "Hello, Recipient!");
			MessageLogic.SendMessage(msg);
		}

		public List<Message> RetrieveMessages()
		{
			KeyStore ks = Session["KeyStore"] as KeyStore;
			return MessageLogic.RetrieveMessages(2, 1, 2, ks);
		}
	}
}