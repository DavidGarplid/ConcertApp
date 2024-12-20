using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.DTO
{
    public class PerformanceDto
    {
        public string ID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime Date { get; set; } 
        public string Location { get; set; } = null!;
        //koppling till Concert (concert?)
    }
}
