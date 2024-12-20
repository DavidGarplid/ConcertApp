using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Entity
{
    public class User
    {
        public required string ID { get; set; }
        string name;
        string email;
        string password;
    }
}
