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
    /// Interaktionslogik für Registration.xaml
    /// </summary>
    public partial class RegistrationView : Page,IView
    {
        private Controller _controller;
        private Frame _frame;

        public RegistrationView(Frame frame)
        {
            InitializeComponent();
            this._frame = frame;
        }

        private void Registrate(object sender, RoutedEventArgs e)
        {
            this._controller.Registrate(TextBoxBenutzername.Text, TextboxPasswort.Text);
        }

        public void Initialize(Controller controller, Model model)
        {
            this._controller = controller;
        }

        public void Update()
        {
            this._frame.Dispatcher.Invoke(() => InvalidateVisual());
        }

        public void Update(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }
    }
}
