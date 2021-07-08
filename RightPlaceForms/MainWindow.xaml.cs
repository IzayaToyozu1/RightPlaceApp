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
            try 
            {
                client.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }    
        }

        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            user = new User();
            user.Name = tbLogin.Text;
            user.Password = tbPassword.Text;
            client.User = user;

            client.ServerCommand(Command.authentication);

            if (LoginFail(ServerGetSet<Command>.GetData(client.Stream)))
            {
                MainMenu menu = new MainMenu(client, user);
                menu.Show();
                this.Close();
            }

            lbInfo.Content = "Неверный пароль или логин";
        }
        private bool LoginFail(Command com) 
        {
            if (com == Command.okUser)
                return true;
            return false;
        } 
    }
}
