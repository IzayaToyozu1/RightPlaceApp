using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using RightPlaceBL.Model;

namespace RightPlaceForms
{
    /// <summary>
    /// Логика взаимодействия для ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {
        internal Chat Chat { get; set; }
        internal ClientObject Clinet { get; set; }
        internal User User { get; set; }
        public ChatControl()
        {
            InitializeComponent();
        }

        private async void btSentMessage_Click(object sender, RoutedEventArgs e)
        {
            lbChat.Items.Add(User.Name + ": " + tbMessage.Text);
            Chat.SentMessage(tbMessage.Text);
            tbMessage.Clear();
        }
        internal void GetMessage()
        {
            Thread thredGetMessage = new Thread(new ThreadStart(ReceiveMessage));
            thredGetMessage.Start();
        }

        private void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    string message = Chat.GetMessage();
                    lbChat.Items.Add(message);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message + User.Name, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Chat.Disconnect();
                }
            }
        } 
    }
}
