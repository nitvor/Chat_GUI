using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    public class Model
    {
        public string UserName { get; private set; }
        public Dictionary<string, Friend> _friendList;
        public DateTime LastLoginTime { get; set; }

        public Model(string userName)
        {
            this.UserName = userName;
            this._friendList = new Dictionary<string, Friend>();
        }

        public void AddFriend(string userName)
        {
            if (!this._friendList.ContainsKey(userName))
                this._friendList.Add(userName, new Friend(userName));
        }

        public void AddMessage(Message message)
        {

                string friend;
                if (message.FromUser == this.UserName) friend = message.ToUser;
                else friend = message.FromUser;
                this._friendList[friend].AddMessage(message);
                if (message.Timestamp > this.LastLoginTime) this._friendList[friend].NewMessage = true;
        }

        public void SetFriendStatus(string friend, bool status)
        {
            this._friendList[friend].Online = status;
        }

        public Dictionary<string, bool> GetFriendList()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (KeyValuePair<string, Friend> valuePair in this._friendList)
            {
                result.Add(valuePair.Key, valuePair.Value.Online);
            }
            return result;
        }

        public bool HaveNewMessageFromUser(string username)
        {
            return this._friendList[username].NewMessage;
        }

        public LinkedList<Message> GetCoverationWithUser(string username)
        {
            return this._friendList[username].GetConversation();
        }
    }
}
