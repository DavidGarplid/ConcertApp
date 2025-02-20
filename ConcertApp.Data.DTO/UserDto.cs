using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.DTO
{
    public class UserDto
    {
        public int ID { get; set; } 
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; }
    }
}
