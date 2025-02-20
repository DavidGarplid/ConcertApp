using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Models
{
    public class Concert
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
