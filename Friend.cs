using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    /// <summary>
    /// Wie ein Freund aussieht.
    /// </summary>
    public class Friend
    {
        /// <summary>
        /// Um den Namen vom Freund zubekommen.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Um zubekommen oder zusetzen ob der Freund Online oder Offline ist.
        /// </summary>
        public bool Online { get; set; }
        
        /// <summary>
        /// Um zubekommen oder zusetzen ob es eine neue Nachricht gibt.
        /// </summary>
        public bool NewMessage { get; set; }

        /// <summary>
        /// Deklaration von Conversation
        /// </summary>
        private LinkedList<Message> _conversation;

        /// <summary>
        /// Um die Conversation mit dem Freund zu bekommen
        /// </summary>
        /// <returns>Die Nachrichten die mit dem Freund geschrieben wurden</returns>
        public LinkedList<Message> GetConversation()
        {
            this.NewMessage = false;
            return _conversation;
        }

        /// <summary>
        /// Attribute eines Freundes werden gesetzt.
        /// </summary>
        /// <param name="name">Name des Freundes</param>
        public Friend(string name)
        {
            this.Name = name;
            this.Online = false;
            this.NewMessage = false;
            this._conversation = new LinkedList<Message>();
        }

        /// <summary>
        /// Nachricht dem Freund hinzufügen.
        /// </summary>
        /// <param name="message">Nachricht</param>
        public void AddMessage(Message message)
        {
            this._conversation.AddLast(message);
            this.NewMessage = true;
        }
    }
}
