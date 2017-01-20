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
		private static Dictionary<int, string> usernames = new Dictionary<int, string>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["KeyStore"] == null)
				Response.Redirect("~/Auth/SignOut?ReturnUrl=/Game/Chat", true);

			if (!IsPostBack)
			{
				RecentsDataList.DataSource = GetRecents();
				RecentsDataList.DataBind();
			}
		}
		
		// 
		protected void ButtonChooseRecipient_Click(object sender, EventArgs e)
		{
			int otherid = -1;
			int currentid = Authenticate.GetUserId();
			string username;

			// Validate Username
			if (string.IsNullOrWhiteSpace(ReceiverId.Text))
			{
				Msg.Text = "Username entered is empty";
				return;
			} else
			{
				otherid = Authenticate.ConvertUsernameToId(ReceiverId.Text);
				username = ReceiverId.Text;
			}

			// Validate UserID exists
			if (otherid == -1)
			{
				Msg.Text = "No recipient by this username was found";
				return;

			} else if (otherid == currentid)
			{
				Msg.Text = "You cannot start a conversation with yourself!";
				return;

			} else if (otherid != currentid && otherid > 0)
			{
				ViewState["thisParty"] = Authenticate.GetUserId();
				ViewState["otherParty"] = otherid;

				ToggleWindows();
				LblRecipient.Text = Authenticate.ConvertIdToUsername(otherid);
				ChatRepeater.DataSource = RetrieveMessages(otherid);
				ChatRepeater.DataBind();
				return;

			}
		}

		protected void SendMsg_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(MessageToSend.Text))
			{
				return;
			}

			int currentuser = Authenticate.GetUserId();
			int otheruser = (int) ViewState["otherParty"];
			var keyStore = Session["KeyStore"] as KeyStore;
			string content = MessageToSend.Text;

			MessageToSend.Text = "";

			content = HttpUtility.HtmlEncode(content);

			Message msg = new Message(currentuser, otheruser, content);
			MessageLogic.SendMessage(msg, keyStore);

			ClientScript.RegisterStartupScript(this.GetType(), "updp", "Update_UpdatePanel()", true);
		}

		protected void ChangeRecipientBtn_Click(object sender, EventArgs e)
		{
			ToggleWindows();
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
		}

		// Utility methods
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
			int id = Authenticate.GetUserId();
			return MessageLogic.RetrieveRecents(id).ToList();
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

		protected void SetRecipient(object sender, EventArgs e)
		{
			ReceiverId.Text = (sender as LinkButton).CommandArgument;
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

		public List<Message> RetrieveMessages(int otherId, int limit = 10)
		{
			if (otherId <= 0)
			{
				return new List<Message>();
			}
			List<Message> msgs = new List<Message>();
			KeyStore ks = Session["KeyStore"] as KeyStore;
			int viewerId = Authenticate.GetUserId();
			//LblRecipient.Text = Authenticate.ConvertIdToUsername(otherId);
			msgs = MessageLogic.RetrieveMessages(viewerId, otherId, ks, limit).ToList();
			return msgs;
		}


	}
}