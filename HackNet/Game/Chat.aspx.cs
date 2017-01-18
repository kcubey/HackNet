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
			if (!IsPostBack)
			{
				if (Session["KeyStore"] == null)
					Response.Redirect("~/Auth/SignOut?ReturnUrl=/Game/Chat", true);
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
				ViewState["thisParty"] = Authenticate.GetUserId();
				ViewState["otherParty"] = userid;
				ChatWindow.Visible = true;
				ChatRepeater.DataSource = RetrieveMessages(username, userid);
				ChatRepeater.DataBind();

				Message msg = new Message(Authenticate.GetUserId(), userid, "Hello, User " + userid);
				MessageLogic.SendMessage(msg);

				return;
			}
		}

		public List<Message> RetrieveMessages(string otherUsername, int otherId)
		{
			List<Message> msgs;
			KeyStore ks = Session["KeyStore"] as KeyStore;
			int viewerId = Authenticate.GetUserId();
			LblRecipient.Text = otherUsername;
			msgs = MessageLogic.RetrieveMessages(viewerId, otherId, viewerId, ks);
			return msgs;
		}

		public string GetUsername(int userid)
		{
			if (!usernames.ContainsKey(userid)) // Caching in dictionary to reduce DB calls
			{
				string username = Authenticate.ConvertIdToUsername(userid);
				usernames.Add(userid, username);
				return username;
			} else
			{
				return usernames[userid];
			}
		}

		public string ThisOrOther(int userid)
		{
			if (userid == (int) ViewState["thisParty"])
			{
				return "self";
			} else
			{
				return "other";
			}
		}
	}
}