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
using System.Windows.Shapes;
using RightPlaceBL.Model;

namespace RightPlaceForms
{
    /// <summary>
    /// Логика взаимодействия для CreateChat.xaml
    /// </summary>
    public partial class CreateChat : Window
    {
        Chat _chat;
        ClientObject _client;
        ChatControl _chatControl;
        Command _com;
        public CreateChat(Chat chat, ClientObject clientObject, ChatControl chatControl, Command command)
        {
            _chat = chat;
            _client = clientObject;
            _chatControl = chatControl;
            _com = command;
            InitializeComponent();
        }

        private void btCreatChat_Click(object sender, RoutedEventArgs e)
        {
            _chat.Name = tbNameChat.Text;
            _client.Chat = _chat;
            _client.ServerCommand(_com);
            _chatControl.Chat = _chat;
            _chatControl.GetMessage();
            this.Close();
        }
    }
}
