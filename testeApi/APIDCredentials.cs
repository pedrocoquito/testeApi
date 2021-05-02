using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testeApi
{
    public class APIDCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public APIDCredentials(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
    }
}
