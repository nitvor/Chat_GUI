﻿using System;
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
    public partial class ChatView : Page, IView
    {
        private Controller _controller;

        private Model _model;
        private Frame _frame;
        private string _sendTo = "";

        public ChatView(Frame frame)
        {
            InitializeComponent();
            this._frame = frame;
        }

        private void Send(object sender, RoutedEventArgs e)
        {
            if (this._sendTo != "") {
                this._controller.SendMessage(_sendTo, TextboxMessage.Text);
                TextboxMessage.Text = "";
            }
            else
            {
                TextboxMessage.Text = "";
            }
        }

        private void AddFriend(object sender, RoutedEventArgs e)
        {
            this._controller.AddFriend(TextboxAddFriend.Text);
            TextboxAddFriend.Text = "";
        }

        public void Initialize(Controller controller, Model model)
        {
            this._controller = controller;
            this._model = model;
        }

        public void Update()
        {
            this._frame.Dispatcher.InvokeAsync(() => fill());
            this._frame.Dispatcher.InvokeAsync(() => InvalidateVisual());
        }

        public void Update(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private void fill()
        {
            if (this._model != null)
            {
                FriendList.Items.Clear();
                foreach (KeyValuePair<string, bool> friend in this._model.GetFriendList())
                {
                    TreeViewItem item = new TreeViewItem();
                    if (friend.Value == true)
                    {
                        item.Foreground = Brushes.Green;
                    }
                    else
                    {
                        item.Foreground = Brushes.Red;
                    }
                    if (this._sendTo == friend.Key)
                    {
                        item.Background = Brushes.LightBlue;
                    }
                    item.Header = friend.Key;
                    item.Name = friend.Key; //Schauen ob noch benötigt
                    item.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(selectedConversation);
                    FriendList.Items.Add(item);
                }

                if (this._sendTo != "")
                {
                    FlowDocument myFlowDoc = new FlowDocument();
                    TextboxChat.Document.Blocks.Clear();
                    foreach (Message m in this._model.GetCoverationWithUser(this._sendTo))
                    {

                        myFlowDoc.Blocks.Add(new Paragraph(new Run(m.Timestamp.ToString() + " " + m.FromUser + " says: ")));
                        myFlowDoc.Blocks.Add(new Paragraph(new Run(m.MessageText)));
                    }
                    TextboxChat.Document = myFlowDoc;
                }

            }
        }

        private void selectedConversation(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = sender as TreeViewItem;
            _sendTo = item.Header as string;
            //this.fill();
            this.Update();
        }

    }
}
