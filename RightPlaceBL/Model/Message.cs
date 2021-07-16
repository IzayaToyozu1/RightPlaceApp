using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Model
{
    public class Message
    {
        public Chat Chat { get; private set; }
        public List<string> Messages { get; set; } = new List<string>();
        
        public Message(Chat chat)
        {
            Chat = chat;
        }
    }
}
