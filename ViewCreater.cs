using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    public interface IViewCreater
    {
          IView GetLogginView(Controller controller);
          IView GetRegistrationView(Controller controller);
          IView GetChatView(Controller controller);
    }
}
