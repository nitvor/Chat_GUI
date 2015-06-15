using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    /// <summary>
    /// Wie eine Nachricht aussieht.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Um zubekommen von wem die Nachricht ist.
        /// </summary>
        public string FromUser { get; private set; }

        /// <summary>
        /// Um zubekommen zu wem die Nachricht geschickt wurde.
        /// </summary>
        public string ToUser { get; private set; }

        /// <summary>
        /// Um die Zeit zubekommen wann die Nachricht geschrieben wurde.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Um den Nachrichten Text zubekommen.
        /// </summary>
        public string MessageText { get; private set; }

        /// <summary>
        /// Setzt die Wert von einer Nachricht
        /// </summary>
        /// <param name="fromUser">Vom wem</param>
        /// <param name="toUser">Zu wem</param>
        /// <param name="timestamp">Zeit wann Nachricht geschickt</param>
        /// <param name="message">Nachricht</param>
        public Message(string fromUser, string toUser, DateTime timestamp, string message)
        {
            this.FromUser = fromUser;
            this.ToUser = toUser;
            this.Timestamp = timestamp;
            this.MessageText = message;
        }
    }
}
