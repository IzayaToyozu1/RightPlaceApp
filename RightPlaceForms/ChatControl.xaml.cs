using System.IO;
using System.Windows;
using System.Windows.Controls;
using RightPlaceBL.Model;

namespace RightPlaceForms
{
    /// <summary>
    /// Логика взаимодействия для ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        public ClientObject Client { get; set; }
        public ChatControl()
        {
            InitializeComponent();
            Client.NewMessage += NewMessage;
        }

        private void BtSentMessage_Click(object sender, RoutedEventArgs e)
        {
            Client.SentMessage(tbMessage.Text);
            tbMessage.Clear();
        }

        private async void NewMessage(string message, Chat chat)
        {
            using (StreamWriter writer = new StreamWriter("Messages_" + chat.Name + ".txt", false))
            {
                await writer.WriteLineAsync(message);
            }
        }

    }
}
