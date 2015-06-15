using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    /// <summary>
    /// Model von einem Chat User.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Um den Username zubekommen
        /// </summary>
        public string UserName { get; private set; }
        /// <summary>
        /// Deklaration der FriendList
        /// </summary>
        public Dictionary<string, Friend> _friendList;
        /// <summary>
        /// Um die letzte Login Zeit zubekommen und zusetzen.
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// Der Username wird gesetzt und friendList wird ein neues Dictionary zugewissen.
        /// </summary>
        /// <param name="userName">Benutzer</param>
        public Model(string userName)
        {
            this.UserName = userName;
            this._friendList = new Dictionary<string, Friend>();
        }

        /// <summary>
        /// Wenn der User ein Freund hinzufügt
        /// </summary>
        /// <param name="userName">Freund</param>
        public void AddFriend(string userName)
        {
            if (!this._friendList.ContainsKey(userName))
                this._friendList.Add(userName, new Friend(userName));
        }

        /// <summary>
        /// Wenn der User eine Nachricht hinzufügt
        /// </summary>
        /// <param name="message">Nachricht</param>
        public void AddMessage(Message message)
        {
                string friend;
                if (message.FromUser == this.UserName) friend = message.ToUser;
                else friend = message.FromUser;
                this._friendList[friend].AddMessage(message);
                if (message.Timestamp > this.LastLoginTime) this._friendList[friend].NewMessage = true;
        }

        /// <summary>
        /// Setzt den Status eines Freundes. Ob Online oder Offline
        /// </summary>
        /// <param name="friend">Freund</param>
        /// <param name="status">Online/Offline</param>
        public void SetFriendStatus(string friend, bool status)
        {
            this._friendList[friend].Online = status;
        }

        /// <summary>
        /// Um die Freundesliste zu hollen.
        /// </summary>
        /// <returns>Freundesliste wird zurückgegeben</returns>
        public Dictionary<string, bool> GetFriendList()
        {
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (KeyValuePair<string, Friend> valuePair in this._friendList)
            {
                result.Add(valuePair.Key, valuePair.Value.Online);
            }
            return result;
        }

        /// <summary>
        /// Abfrage ob man von dem User eine neue Nachricht hat.
        /// </summary>
        /// <param name="username">Freund</param>
        /// <returns>Ob man eine neue Nachricht bekommen hat</returns>
        public bool HaveNewMessageFromUser(string username)
        {
            return this._friendList[username].NewMessage;
        }

        /// <summary>
        /// Um die Converation mit einem User zuerhalten.
        /// </summary>
        /// <param name="username">Freund</param>
        /// <returns>Gibt die Nachrichten mit dem User zurück</returns>
        public LinkedList<Message> GetCoverationWithUser(string username)
        {
            return this._friendList[username].GetConversation();
        }
    }
}
