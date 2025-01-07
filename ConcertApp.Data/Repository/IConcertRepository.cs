using ConcertApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Repository
{
    public interface IConcertRepository : IRepository<Concert>
    {
        public Task<Concert> Find(string id);
        public Task UpdateAsync(Concert concert);

        public Task CreateAsync(Concert concert);
    }
}
