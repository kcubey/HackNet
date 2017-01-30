using HackNet.Security;
using Microsoft.AspNet.SignalR;
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
		private static Dictionary<int, string> usernames = new Dictionary<int, string>();

		/*
		 *  EVENT BASED METHODS
		 */
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["KeyStore"] == null)
				Response.Redirect("~/Auth/SignOut?ReturnUrl=/Game/Chat", true);

			MessageToSend.MaxLength = 11000;

			if (!IsPostBack)
			{
				RecentsDataList.DataSource = GetRecents();
				RecentsDataList.DataBind();
			}
		}
		

		protected void SendMsg_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(MessageToSend.Text))
				return;

			if (Msg1.Text != "")
				Msg1.Text = "";

			if (MessageToSend.Text.Length > 10000)
			{
				Msg1.Text = "Message is too long! (Max 10000 characters)";
				return;
			}

			int currentuser = CurrentUser.GetUserId();
			int otheruser = (int) ViewState["otherParty"];
			var keyStore = Session["KeyStore"] as KeyStore;
			string content = MessageToSend.Text;

			MessageToSend.Text = "";

			content = HttpUtility.HtmlEncode(content);

			Message msg = new Message(currentuser, otheruser, content);
			MessageLogic.SendMessage(msg, keyStore);

			Microsoft.AspNet.SignalR.Messaging.MessageHub.ServerCauseRefresh();
		}

		protected void ChangeRecipientBtn_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/Game/Chat");
		}

		protected void ChatWindow_Load(object sender, EventArgs e)
		{
			if (Session["KeyStore"] == null)
				Msg.Text = "Error decrypting messages, KS is NULL!";
		}

		protected void ChatRepeater_Load(object sender, EventArgs e)
		{
			base.OnLoad(e);
			int otherid;

			if (ViewState["otherParty"] != null)
				otherid = (int)ViewState["otherParty"];
			else
				otherid = -1;

			ChatRepeater.DataSource = RetrieveMessages(otherid);
			ChatRepeater.DataBind();

			if (ChatWindow.Visible)
				ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "javascript:updateScroll();", true);
		}

		protected void ButtonChooseRecipient_Click(object sender, EventArgs e)
		{
			int otherid = -1;
			int currentid = CurrentUser.GetUserId();
			string username;

			// Validate Username
			if (string.IsNullOrWhiteSpace(ReceiverId.Text))
			{
				Msg.Text = "Username entered is empty";
				return;
			}
			else
			{
				otherid = Authenticate.ConvertUsernameToId(ReceiverId.Text);
				username = ReceiverId.Text;
			}

			// Validate UserID exists
			if (otherid == -1)
			{
				Msg.Text = "No recipient by this username was found";
				return;

			}
			else if (otherid == currentid)
			{
				Msg.Text = "You cannot start a conversation with yourself!";
				return;

			}
			else if (otherid != currentid && otherid > 0)
			{
				ViewState["thisParty"] = CurrentUser.GetUserId();
				ViewState["otherParty"] = otherid;

				ToggleWindows();
				LblRecipient.Text = Authenticate.ConvertIdToUsername(otherid);
				ChatRepeater.DataSource = RetrieveMessages(otherid);
				ChatRepeater.DataBind();
				return;
			}
		}


		protected void SetRecipient(object sender, EventArgs e)
		{
			if (sender is LinkButton)
			{
				string recipient = (sender as LinkButton).CommandArgument;
				if (recipient.Contains(" (UNREAD)"))
					ReceiverId.Text = recipient.Replace(" (UNREAD)", "");
				else
					ReceiverId.Text = recipient;

				ButtonChooseRecipient_Click(sender, e);
			}
		}

		/*
		 * OTHER METHODS
		 */
		public List<Message> RetrieveMessages(int otherId, int limit = 10)
		{
			if (otherId <= 0)
			{
				return new List<Message>();
			}
			List<Message> msgs = new List<Message>();
			KeyStore ks = Session["KeyStore"] as KeyStore;
			int viewerId = CurrentUser.GetUserId();
			//LblRecipient.Text = Authenticate.ConvertIdToUsername(otherId);
			msgs = MessageLogic.RetrieveMessages(viewerId, otherId, ks).ToList();
			return msgs;
		}

		public string ThisOrOther(int userid)
		{
			if (userid == (int)ViewState["thisParty"])
			{
				return "self";
			}
			else
			{
				return "other";
			}
		}

		protected List<string> GetRecents()
		{
			int id = CurrentUser.GetUserId();
			var list = new List<string>();
			var dict = new Dictionary<string, bool>(MessageLogic.RetrieveRecents(id));
			foreach (var each in dict)
			{
				if (each.Value == true)
					list.Add(each.Key + " (UNREAD)");
				else
					list.Add(each.Key);
			}

			return list;
		}

		protected void ToggleWindows()
		{
			if (SelectRecipientWindow.Visible == true)
			{
				SelectRecipientWindow.Visible = false;
				ChatWindow.Visible = true;
			}
			else
			{
				SelectRecipientWindow.Visible = true;
				ChatWindow.Visible = false;
			}
		}



		public string GetUsername(int userid)
		{
			if (!usernames.ContainsKey(userid)) // Caching in dictionary to reduce DB calls
			{
				string username = Authenticate.ConvertIdToUsername(userid);
				usernames.Add(userid, username);
				return username;
			}
			else
			{
				return usernames[userid];
			}
		}




	}
}