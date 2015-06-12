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
    /// Interaktionslogik für LoginView.xaml
    /// </summary>
    public partial class LoginView : Page, IView
    {
        private Controller _controller;

        public LoginView()
        {
            InitializeComponent();
        }

        public void Initialize(Controller controller, Model model)
        {
            this._controller = controller;
        }

        public void Update()
        {
            InvalidateVisual();
        }

        public void Update(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            this._controller.LogIn(TextboxBenutzername.Text, TextboxPasswort.Password);
        }

        private void GetRegistrationView(object sender, RoutedEventArgs e)
        {
            this._controller.GetRegistrationView();
        }
    }
}
