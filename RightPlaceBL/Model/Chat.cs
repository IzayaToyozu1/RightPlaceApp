using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using RightPlaceBL.Service;

namespace RightPlaceBL.Model
{
    public class Chat
    {
        public string Name { get; set; }
        public User User { get; set; }

        TcpClient _chatClient = new TcpClient();
        string _address = "127.0.0.1";
        internal int Port { get; set; }
        public NetworkStream Stream { get; private set; }

        public void ConnectionChat()
        {
            _chatClient.Connect(_address, Port);
            Stream =  _chatClient.GetStream();
            ServerGetSet<User>.SentDataStrem(Stream, User);
        }

        public string GetMessage()
        {
            return ServerGetSet<string>.GetDataStream(Stream);
        }

        public void SentMessage(string message)
        {
            ServerGetSet<string>.SentString(message, Stream);
        }
        public void Disconnect()
        {
            if (Stream != null)
                Stream.Close();
            if (_chatClient != null)
                _chatClient.Close();
        }
    }
}

