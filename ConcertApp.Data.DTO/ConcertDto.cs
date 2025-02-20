using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.DTO
{
    public class ConcertDto
    {
        public int? ID { get; set; } 
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        //koppling till performance (performances?)
    }
}
