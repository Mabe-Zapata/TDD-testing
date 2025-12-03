using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCore
{
    public class User
    {
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public User(string code, string firstName, string lastName)
        {
            Code = code;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}