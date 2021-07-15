using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RightPlaceBL.Model;
using RightPlaceBL.Service;

namespace RightPlaceForms
{
    /// <summary>
    /// Логика взаимодействия для MainRegistration.xaml
    /// </summary>
    public partial class MainRegistration : Window
    {
        ClientObject _client;

        public MainRegistration(ClientObject client)
        {
            _client = client;
            InitializeComponent();
        }

        private void btRegistration_Click(object sender, RoutedEventArgs e)
        {
            User user = new User()
            {
                Name = tbName.Text,
                Login = tbLogin.Text,
                Password = tbPassword.Text,
                BirthDate =  DateTime.Parse(tbBirthData.Text),
                Email = tbEmail.Text
            };
            _client.User = user;
            _client.ServerCommand(Command.registration);
            this.Close();
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
