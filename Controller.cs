using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Chat_GUI
{
    public class Controller
    {
        private Model _model;

        private Listener _listener;

        private IView _view;

        private IViewCreater _creater;

        private string _userName;

        private object semaphore = new object();

        public Controller(IViewCreater creater)
        {
            this._listener = new Listener(this);
            this._creater = creater;
            this._view = _creater.GetLogginView(this);
            this._model = null;
            this._userName = null;
            this._view.Update();
        }

        public Model GetModel()
        {
            lock (this.semaphore)
            {
                return _model;
            }
        }

        public void GetRegistrationView()
        {
            this._view = _creater.GetRegistrationView(this);
            this._view.Update();
        }

        public void Receive(XElement element)
        {
            lock (this.semaphore)
            {
                if (element != null)
                {
                    if (element.Name.ToString() == "error")
                    {
                        this._view.Update(element.Value);
                    }
                    else
                    {
                        if (this._model == null)
                        {
                            this._model = new Model(this._userName);
                            this._view = _creater.GetChatView(this);
                        }
                        if (element.Name.ToString() == "friendList")
                        {
                            foreach (XElement friend in element.Nodes())
                            {
                                this._model.AddFriend(friend.Value.TrimEnd());
                                this._model.SetFriendStatus(friend.Value.TrimEnd(), Convert.ToBoolean(friend.Attribute("online").Value));
                            }
                        }
                        else if (element.Name.ToString() == "lastLogIn")
                        {
                            this._model.LastLoginTime = Convert.ToDateTime(element.Value);
                        }
                        else if (element.Name.ToString() == "message")
                        {
                            Message m = new Message(element.Attribute("from").Value.TrimEnd(), element.Attribute("to").Value.TrimEnd(), Convert.ToDateTime(element.Attribute("time").Value), element.Value);
                            this._model.AddMessage(m);
                        }
                    }
                    this._view.Update();
                }
            }
        }

        public void LogIn(string userName, string pass)
        {
            lock (this.semaphore)
            {
                this.reload();
                this._userName = userName;
                XElement e = new XElement("logIn");
                e.Add(new XElement("name", userName));
                e.Add(new XElement("pwd", pass));
                this._listener.Send(e);
            }
        }

        public void Registrate(string userName, string pass)
        {
            lock (this.semaphore)
            {
                this.reload();
                this._userName = userName;
                XElement e = new XElement("registrate");
                e.Add(new XElement("name", userName));
                e.Add(new XElement("pwd", pass));
                _listener.Send(e);
            }
        }


        public void AddFriend(string userName)
        {
            lock (this.semaphore)
            {
                XElement e = new XElement("addFriend", userName);
                _listener.Send(e);
            }
        }

        public void SendMessage(string toUser, string messageText)
        {
            lock (this.semaphore)
            {
                XElement e = new XElement("message", messageText);
                e.SetAttributeValue("to", toUser);
                _listener.Send(e);
            }
        }

        public void LogOut()
        {
            lock (this.semaphore)
            {
                XElement e = new XElement("logOut");
                _listener.Send(e);
            }
        }

        private void reload()
        {
            if (this._model != null)
            {
                this.LogOut();
                //this._listener = new Listener(this);
            }
        }
    }
}
