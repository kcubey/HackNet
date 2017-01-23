using System;

namespace Microsoft.AspNet.SignalR.Messaging
{
    public class MessageSignal
    {

        public int SenderId { get; set; }
        
        public string SenderName { get; private set; }
        
        public int ReceiverId { get; private set; }
        
        public string ReceiverName { get; private set; }

        public DateTime Timestamp { get; private set; }
      
    }
}
