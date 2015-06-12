using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat_GUI
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewCreater
    {

        private ChatView _chatView;
        public MainWindow()
        {
            InitializeComponent();
            new Controller(this);
            this._chatView = new ChatView();
        }

        public IView GetLogginView(Controller controller)
        {
            IView logInView = new LoginView();
            logInView.Initialize(controller, controller.GetModel());
            Frame.Navigate((Page)logInView);
            return logInView;
        }

        public IView GetRegistrationView(Controller controller)
        {
            IView registrationView = new RegistrationView();
            registrationView.Initialize(controller, controller.GetModel());
            Frame.Navigate((Page)registrationView);
            return registrationView;
        }

        public IView GetChatView(Controller controller)
        {
            IView chatView = this._chatView;
            chatView.Initialize(controller, controller.GetModel());
            Frame.Dispatcher.Invoke(() => Frame.Navigate((Page)chatView));
            return chatView;
        }

    }

}
