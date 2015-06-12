using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_GUI
{
    public interface IView
    {
         void Initialize(Controller controller, Model model);
         void Update();
         void Update(string errorMessage);
    }
}
