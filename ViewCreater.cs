using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    /// <summary>
    /// Erzeugt die Fenster Login,Chat und Registration.
    /// </summary>
    public interface IViewCreater
    {
        /// <summary>
        /// Hier wird das Loginfenster erzeugt und zurückgegeben.
        /// </summary>
        /// <param name="controller">Clientsteuerung</param>
        /// <returns>Gibt das Loginfenster zurück</returns>
        IView GetLogginView(Controller controller);
        
        /// <summary>
        /// Hier wird das Registrationfenster erzeugt und zurückgegeben.
        /// </summary>
        /// <param name="controller">Clientsteuerung</param>
        /// <returns>Gibt das Registrationfenster zurück</returns>
        IView GetRegistrationView(Controller controller);
        
        /// <summary>
        /// Hier wird das Chatfenster erzeugt und zurückgegeben.
        /// </summary>
        /// <param name="controller">Clientsteuerung</param>
        /// <returns>Gibt das Loginfenster zurück</returns>
        IView GetChatView(Controller controller);
    }
}
