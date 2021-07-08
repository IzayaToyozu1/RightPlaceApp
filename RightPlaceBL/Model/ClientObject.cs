using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using RightPlaceBL.Service;

namespace RightPlaceBL.Model
{
    public class ClientObject
    {
        private const string _address = "127.0.0.1";
        private const int _portClient = 8888;
        static TcpClient _client;
        public NetworkStream Stream { get; private set; }
        public User User { get; set; }
        public Chat Chat { get; set; }
        private List<Chat> _chats = new List<Chat>(); 

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
            command = ServerGetSet<Command>.GetData(Stream);

            switch (command)
            {
                case Command.authentication:
                    ServerGetSet<User>.SentDataStrem(Stream, User);
                    break;

                case Command.registration:

                    break;

                case Command.createChat:
                    ServerGetSet<string>.SentString(Chat.Name ,Stream);
                    int port = Convert.ToInt32(ServerGetSet<int>.GetDataStream(Stream));
                    Chat.Port = port;
                    Chat.User = User;
                    Chat.ConnectionChat();
                    _chats.Add(Chat);
                    break;

                case Command.addChat:
                    ServerGetSet<string>.SentString(Chat.Name, Stream);
                    port = Convert.ToInt32(ServerGetSet<int>.GetDataStream(Stream));
                    Chat.Port = port;
                    Chat.User = User;
                    Chat.ConnectionChat();
                    _chats.Add(Chat);
                    break;
            }
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
        leaveChat
    }
}