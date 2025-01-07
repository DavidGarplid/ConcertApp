using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Entity
{
    public class Performance
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime DateTime { get; set; }
        public int ConcertId { get; set; }
        public Concert Concert { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
