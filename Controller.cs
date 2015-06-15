using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Chat_GUI
{
    /// <summary>
    /// Steuerung von der GUI.
    /// Sendet und bekommt die Daten vom Listener.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Deklaration der Variablen
        /// </summary>
        private Model _model;
        private Listener _listener;
        private IView _view;
        private IViewCreater _creater;
        private string _userName;
        private object semaphore = new object();

        /// <summary>
        /// Muss ein IViewCreater Objekt übergeben werden.
        /// Setzt die Variablen und ruft von view die Update()-Methode auf.
        /// </summary>
        /// <param name="creater"></param>
        public Controller(IViewCreater creater)
        {
            this._listener = new Listener(this);
            this._creater = creater;
            this._view = _creater.GetLogginView(this);
            this._model = null;
            this._userName = null;
            this._view.Update();
        }

        /// <summary>
        /// Um das Model vom User zubekommen.
        /// </summary>
        /// <returns>Gibt das Model vom User zurück</returns>
        public Model GetModel()
        {
            lock (this.semaphore)
            {
                return _model;
            }
        }

        /// <summary>
        /// Hier wird die View auf die RegistrationView geändert und mit view die Update()-Methode aufgerufen.
        /// </summary>
        public void GetRegistrationView()
        {
            this._view = _creater.GetRegistrationView(this);
            this._view.Update();
        }

        /// <summary>
        /// Die werden die Daten die wir vom Server bekommen haben ausgewertet.
        /// </summary>
        /// <param name="element">Server Daten</param>
        public void Receive(XElement element)
        {
            lock (this.semaphore)
            {
                if (element != null)
                {
                    /*
                     * Wenn der Server ein Error schickt, wird mit view die Update()-Methode augerufen,
                     * der ein String übergeben werden muss, in dem der Fehler steht.
                     */
                    if (element.Name.ToString() == "error")
                    {
                        this._view.Update(element.Value);
                    }
                    else
                    {
                        /*
                         * Wenn Registration erfolgreich war, wird automatisch das ChatFenster geöffnet.
                         */
                        if (this._model == null)
                        {
                            this._model = new Model(this._userName);
                            this._view = _creater.GetChatView(this);
                        }
                        /*
                         * Die Freundesliste wird dem Model hinzugefügt und der Status wird gesetzt.
                         */
                        if (element.Name.ToString() == "friendList")
                        {
                            foreach (XElement friend in element.Nodes())
                            {
                                this._model.AddFriend(friend.Value.TrimEnd());
                                this._model.SetFriendStatus(friend.Value.TrimEnd(), Convert.ToBoolean(friend.Attribute("online").Value));
                            }
                        }
                        /*
                         * Das letzte Login wird im Model gespeichert.
                         */
                        else if (element.Name.ToString() == "lastLogIn")
                        {
                            this._model.LastLoginTime = Convert.ToDateTime(element.Value);
                        }
                        /*
                         * Die Nachrichten werden dem Model hinzugefügt.
                         */
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

        /// <summary>
        /// Hier sagen wir dem Server, dass wir uns einloggen wollen.
        /// Dafür übergeben wir den Username und das Passwort.
        /// </summary>
        /// <param name="userName">Benutzername</param>
        /// <param name="pass">Passwort</param>
        public void LogIn(string userName, string pass)
        {
            lock (this.semaphore)
            {
                if (this._model != null)
                {
                    this._model = null;
                    this.LogOut();
                }
                this._userName = userName;
                XElement e = new XElement("logIn");
                e.Add(new XElement("name", userName));
                e.Add(new XElement("pwd", pass));
                this._listener.Send(e);
            }
        }

        /// <summary>
        /// Hier sagen wir dem Server, dass wir uns Registrieren.
        /// Dafür müssen wir den Username und das Passwort übergeben.
        /// </summary>
        /// <param name="userName">Benutzername</param>
        /// <param name="pass">Passwort</param>
        public void Registrate(string userName, string pass)
        {
            lock (this.semaphore)
            {
                if (this._model != null)
                {
                    this._model = null;
                    this.LogOut();
                }
                this._userName = userName;
                XElement e = new XElement("registrate");
                e.Add(new XElement("name", userName));
                e.Add(new XElement("pwd", pass));
                _listener.Send(e);
            }
        }

        /// <summary>
        /// Wenn sagen wir dem Server, dass wir ein Freund zur Freundesliste hinzufügen.
        /// Dafür müssen wir den Username des Freundes übergeben.
        /// </summary>
        /// <param name="userName">Freund</param>
        public void AddFriend(string userName)
        {
            lock (this.semaphore)
            {
                XElement e = new XElement("addFriend", userName);
                _listener.Send(e);
            }
        }

        /// <summary>
        /// Hier senden wir die Nachricht zum Server.
        /// Dafür müssen wir sagen zu wem die Nachricht geschickt wird und der Text.
        /// </summary>
        /// <param name="toUser">Zu Wem</param>
        /// <param name="messageText">Nachricht</param>
        public void SendMessage(string toUser, string messageText)
        {
            lock (this.semaphore)
            {
                XElement e = new XElement("message", messageText);
                e.SetAttributeValue("to", toUser);
                _listener.Send(e);
            }
        }

        /// <summary>
        /// Hier sagen wir dem Server, dass wir uns Ausloggen.
        /// </summary>
        public void LogOut()
        {
            lock (this.semaphore)
            {
                XElement e = new XElement("logOut");
                _listener.Send(e);
            }
        }
    }
}
