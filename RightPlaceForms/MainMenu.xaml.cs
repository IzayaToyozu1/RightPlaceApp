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
using RightPlaceBL.Service;

namespace RightPlaceForms
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        ClientObject _client;
        User _user;

        public MainMenu(ClientObject client, User user)
        {
            _client = client;
            _user = user;
            InitializeComponent();
        }

        private void btCreatChat_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            CreateChat createChat = new CreateChat(chat, _client, ControlChat, Command.createChat);
            ControlChat.User = _user;
            createChat.Show();
        }

        private void btAddChat_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            CreateChat createChat = new CreateChat(chat, _client, ControlChat, Command.addChat);
            ControlChat.User = _user;
            createChat.Show();
        }
    }
}
