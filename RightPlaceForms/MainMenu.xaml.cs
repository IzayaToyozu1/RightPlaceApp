using System.IO;
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

        public MainMenu(ClientObject client)
        {
            _client = client;
            if (_client.User.Chats != null)
            {
                foreach (var chat in _client.User.Chats)
                {
                    lvChatNames.Items.Add(chat.Name);
                }
            }
            InitializeComponent();
        }

        private void BuildChat()
        {
            _client.Chat = _client.User.Chats.FirstOrDefault(c => c.Name == (string)lvChatNames.SelectedItem);
            ControlChat.lbChat.Items.Clear();
            using(StreamReader reader = new StreamReader("Message_" + _client.Chat.Name + ".txt"))
            {
                ControlChat.lbChat.Items.Add(reader);
            }
        }

        private void btCreatChat_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            _client.User.Chats.Add(chat);
            CreateChat createChat = new CreateChat(chat, _client, Command.createChat);
            createChat.Show();
        }

        private void btAddChat_Click(object sender, RoutedEventArgs e)
        {
            Chat chat = new Chat();
            _client.User.Chats.Add(chat);
            CreateChat createChat = new CreateChat(chat, _client, Command.addChat);
            createChat.Show();
        }

    }
}
