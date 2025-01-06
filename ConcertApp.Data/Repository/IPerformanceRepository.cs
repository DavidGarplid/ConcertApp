using ConcertApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Repository
{
    public interface IPerformanceRepository : IRepository<Performance>
    {
        Task<Performance?> Find(string id);
        public Task UpdateAsync(Performance performance);
        public Task CreateAsync(Performance performance);
    }
}
