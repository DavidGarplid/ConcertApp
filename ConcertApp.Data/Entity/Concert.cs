using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Entity
{
    public class Concert
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Performance Performance { get; set; }
    }
}
