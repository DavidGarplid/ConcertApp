using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.DTO
{
    public class ConcertDto
    {
        public string ID { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        //koppling till performance (performances?)
    }
}
