using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int UserID { get; set; }
        public int PerformanceID { get; set; }

    }
}
