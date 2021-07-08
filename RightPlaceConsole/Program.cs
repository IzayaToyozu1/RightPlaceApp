using System;
using RightPlaceBL.Model;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Text;
using System.Text.Json;


namespace RightPlaceConsole
{
    class Program
    {

        static Chat _chat;
        Command command = new Command();
        static void Main(string[] args)
        {
            //ClientObject _client = new ClientObject();
            //_client.Start();
            //Console.WriteLine("Выберите команду: \nр - регстрация;\nа-авторизация");
            //string com = Console.ReadLine();
            //Command command = new Command();

            //switch (com)
            //{
            //    case "р":

            //        Disconnect(_client);

            //        break;
            //    case "а":
            //        User user = new User();
            //        Console.WriteLine("Введите логин");
            //        user.Name = Console.ReadLine();
            //        Console.WriteLine("Введите пароль");
            //        user.Password = Console.ReadLine();
            //        if (_client.Authentication(command, user) == Command.authentication)
            //        {
            //            Console.WriteLine("+");
            //            Thread receiveThread = new Thread(new ParameterizedThreadStart(ReceiveMessage));
            //            receiveThread.Start(_client);
            //            _chat = new Chat(_client);
            //            while (true)
            //            {
            //                string message = Console.ReadLine();
            //                Console.WriteLine(user.Name + ": " + message);
            //                _chat.SendMessage(message);
            //            }
            //        }
            //        Disconnect(_client);
            //        break;
            //}
        }

        private static void ReceiveMessage(object client)
        {
            ClientObject _client = (ClientObject)client;
            while (true)
            {
                try
                {
                    byte[] data = new byte[64];
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = _client.Stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (_client.Stream.DataAvailable);

                    string message = builder.ToString();
                    Console.WriteLine(message);

                }
                catch(Exception ex)
                {
                    Console.WriteLine("Подключение прервано!"); //соединение было прервано
                    Console.ReadLine();
                    Disconnect(_client);
                }
            }
        }
        static void Disconnect(ClientObject client)
        {
            if (client.Stream != null)
                client.Stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }
    }
}
