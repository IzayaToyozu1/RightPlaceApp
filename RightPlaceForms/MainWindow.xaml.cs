using System;
using System.Threading;
using System.Windows;
using RightPlaceBL.Model;
using RightPlaceBL.Service;

namespace RightPlaceForms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientObject client;
        User user;
        public MainWindow()
        {
            InitializeComponent();
            client = new ClientObject();
            Thread getCommandThread = new Thread(new ThreadStart(client.CommandsServer));
            getCommandThread.Start();
            try 
            {
                client.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            client.UserNull += UserNull;
        }

        private void UserNull()
        {
            lbInfo.Content = "Логин или пароль неверный";
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            client.User.Login = tbLogin.Text;
            client.User.Password = tbPassword.Text; 

            client.ServerCommand(Command.authentication);
        }

        private void btRegistration_Click(object sender, RoutedEventArgs e)
        {
            MainRegistration registration = new MainRegistration(client);
            registration.ShowDialog();
        }
    }
}
