using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    /// <summary>
    /// Erzeugt und Update die Fenster Login,Chat und Registration.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Erzeugt das Fenster.
        /// Ihm muss ein Controller übergeben werden und das Model
        /// </summary>
        /// <param name="controller">Clientsteuerung</param>
        /// <param name="model">Model vom User</param>
        void Initialize(Controller controller, Model model);
        
        /// <summary>
        /// Updatet das Fenster
        /// </summary>
        void Update();
        
        /// <summary>
        /// Updatet das Fenster mit der Fehlermeldung die wir vom Server erhalten haben.
        /// </summary>
        /// <param name="errorMessage">Fehlermeldung</param>
        void Update(string errorMessage);
    }
}
