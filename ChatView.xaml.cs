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
    /// Interaktionslogik für ChatView.xaml
    /// </summary>
    public partial class ChatView : Page,IView
    {
        private Controller _controller;

        private Model _model;
        private Frame _frame;

        public ChatView(Frame frame)
        {
            InitializeComponent();
            this._frame = frame;
        }

        private void Send(object sender, RoutedEventArgs e)
        {

        }

        private void AddFriend(object sender, RoutedEventArgs e)
        {

        }

        public void Initialize(Controller controller, Model model)
        {
            this._controller = controller;
            this._model = model;
        }

        public void Update()
        {
            this._frame.Dispatcher.Invoke(() => fill());
            this._frame.Dispatcher.Invoke(()=>InvalidateVisual());
        }

        public void Update(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void fill()
        {
            if (this._model != null)
            {
                foreach (KeyValuePair<string, bool> friend in this._model.GetFriendList())
                {
                    foreach (Message m in this._model.GetCoverationWithUser(friend.Key))
                    {
                        TextboxMessage.SetValue(TextBlock.TextProperty, m.MessageText);
                    }
                }
            }
        }
    }
}
