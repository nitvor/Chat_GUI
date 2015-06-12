using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    public class Message
    {
        public string FromUser { get; private set; }

        public string ToUser { get; private set; }

        public DateTime Timestamp { get; private set; }

        public string MessageText { get; private set; }

        public Message(string fromUser, string toUser, DateTime timestamp, string message)
        {
            this.FromUser = fromUser;
            this.ToUser = toUser;
            this.Timestamp = timestamp;
            this.MessageText = message;
        }
    }
}
