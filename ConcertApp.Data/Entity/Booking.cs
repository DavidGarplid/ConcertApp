using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Entity
{
    public class Booking
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Performance Performance { get; set; }
        public string PerformanceID { get; set; }
        
    }
}
