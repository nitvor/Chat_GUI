using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    public class Friend
    {
        public string Name { get; private set; }
        public bool Online { get; set; }
        public bool NewMessage { get; set; }
        private LinkedList<Message> _conversation;

        public LinkedList<Message> GetConversation()
        {
            this.NewMessage = false;
            return _conversation;
        }


        public Friend(string name)
        {
            this.Name = name;
            this.Online = false;
            this.NewMessage = false;
            this._conversation = new LinkedList<Message>();
        }

        public void AddMessage(Message message)
        {
            this._conversation.AddLast(message);
            this.NewMessage = true;
        }
    }
}
