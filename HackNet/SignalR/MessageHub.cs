using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR.Hubs;

namespace Microsoft.AspNet.SignalR.Messaging
{
    [HubName("ChatClientz")]
    public class MessageHub : Hub
    {
        public MessageHub()
        {

        }

        public void SendMessage(string name, string msg)
        {
			Clients.All.broadcastMessage(name, msg);
        }

		public void CauseRefresh()
		{
			Clients.All.doRefresh();
		}

		public static void ServerCauseRefresh()
		{
			var signalrcon = GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
			signalrcon.Clients.All.doRefresh();
		}
	}
}