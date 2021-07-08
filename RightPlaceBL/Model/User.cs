using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightPlaceBL.Model
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age
        {
            get
            {
                return BirthDate.Hour;
            }
        }
        public DateTime BirthDate { get; private set; }

        public string Password { get; set; }
        public User() { }
        public User(string name, DateTime birthDate, string password)
        {
            Name = name;
            BirthDate = birthDate;
            Password = password;
        }
    }
}
