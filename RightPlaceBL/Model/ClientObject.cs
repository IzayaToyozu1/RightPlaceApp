using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using RightPlaceBL.Service;

namespace RightPlaceBL.Model
{
    public class ClientObject
    {
        public delegate void GetMessage(string message, Chat chat);
            
        public event Action UserNull;
        public event Action UserLoggedIn;
        public event GetMessage NewMessage;

        private const string _address = "127.0.0.1";
        private const int _portClient = 8888;
        static TcpClient _client;
        private string _message;

        public NetworkStream Stream { get; private set; }
        public Chat Chat { get; set; }
        public User User { get; internal set; } = new User();


        public ClientObject()
        {
            _client = new TcpClient();
        }

        public void Start()
        {
            _client.Connect(_address, _portClient);
            Stream = _client.GetStream();
        }

        public void ServerCommand(Command command)
        {
            ServerGetSet<Command>.SentDataStrem(Stream, command);
        }

        public void CommandsServer()
        {
            while (true)
            {
                var command = ServerGetSet<Command>.GetData(Stream);
                switch (command)
                {
                    case Command.notUser:
                        UserNull?.Invoke();
                        break;

                    case Command.okUser:
                        User.Chats = ServerGetSet<List<Chat>>.GetData(Stream);
                        UserLoggedIn?.Invoke();
                        break;

                    case Command.getMessageChat:
                        string nameChat = ServerGetSet<string>.GetDataStream(Stream);
                        _message = ServerGetSet<string>.GetDataStream(Stream);
                        var chat = User.Chats.FirstOrDefault(c => c.Name == nameChat);
                        NewMessage?.Invoke(_message, chat);
                        _message = "";
                        break;
                    default:
                        CommandClient(command);
                        break;
                }
            }
        }

        public void CommandClient(Command command)
        {
            switch (command)
            {
                case Command.authentication:
                    ServerGetSet<User>.SentDataStrem(Stream, User); 
                    break;

                case Command.registration:
                    ServerGetSet<User>.SentDataStrem(Stream, User);
                    break;

                case Command.createChat:
                    ServerGetSet<string>.SentString(Chat.Name, Stream);
                    User.Chats.Add(Chat);
                    break;

                case Command.addChat:
                    ServerGetSet<string>.SentString(Chat.Name, Stream);
                    User.Chats.Add(Chat);
                    break;

                case Command.leaveChat:
                    ServerGetSet<string>.SentString(Chat.Name, Stream);
                    User.Chats.Remove(Chat);
                    break;

                case Command.messageChat:
                    ServerGetSet<Chat>.SentString(Chat.Name, Stream);
                    ServerGetSet<string>.SentString(User.Name + ": " + _message, Stream);
                    _message = "";
                    break;
            }
        }

        public void Registration(User user)
        {
            User = user;
            ServerCommand(Command.registration);
        }

        public void SentMessage(string message)
        {
            ServerCommand(Command.messageChat);
            _message = message;
        }

        public void Close()
        {
            _client.Close();
        }
    }

    public enum Command
    {
        authentication,
        notUser,
        okUser,
        registration,
        createChat,
        addChat,
        leaveChat,
        messageChat,
        getMessageChat
    }
}