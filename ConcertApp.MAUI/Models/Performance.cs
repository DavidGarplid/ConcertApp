using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Models
{
    public class Performance
    {
        public int ID { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;
        public int ConcertId { get; set; }
        public bool IsBooked { get; set; }
    }
}
