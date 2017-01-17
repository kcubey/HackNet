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
		private int otherUserId { get; set; }
		private string otherUsername { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (Session["KeyStore"] == null)
					Response.Redirect("~/Default", true);

				Message msg = new Message(Authenticate.GetUserId(), 1, "Hello, Recipient!");
				MessageLogic.SendMessage(msg);
			}
		}
		
		protected void ButtonChooseRecipient_Click(object sender, EventArgs e)
		{
			int userid = -1;
			string username;

			// Validate Username
			if (string.IsNullOrWhiteSpace(ReceiverId.Text))
			{
				Msg.Text = "Username entered is empty";
				return;
			} else
			{
				userid = Authenticate.ConvertUsernameToId(ReceiverId.Text);
				username = ReceiverId.Text;
			}

			// Validate UserID
			if (userid == -1)
			{
				Msg.Text = "No recipient by this username was found";
				return;
			} else
			{
				otherUserId = userid;
				otherUsername = username;
				return;
			}
		}

		protected void ButtonLoadChat_Click(object sender, EventArgs e)
		{
			ChatDataList.DataBind();
		}

		public List<Message> RetrieveMessages()
		{
			KeyStore ks = Session["KeyStore"] as KeyStore;
			int viewerId = Authenticate.GetUserId();
			int otherId = otherUserId;
			// int otherId = Authenticate.ConvertUsernameToId(otherUsername);
			LblRecipient.Text = otherUsername;
			return MessageLogic.RetrieveMessages(viewerId, otherId, viewerId, ks);
		}
	}
}