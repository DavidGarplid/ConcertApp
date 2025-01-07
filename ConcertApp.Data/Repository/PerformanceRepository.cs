using ConcertApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Repository
{
    public class PerfomanceRepository : Repository<Performance>, IPerformanceRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public PerfomanceRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public async Task UpdateAsync(Performance performance)
        {
            if (performance == null)
            {
                throw new ArgumentNullException(nameof(performance));
            }
            var existingBooking = await Context.Set<Performance>().FindAsync(performance.ID);
            Context.Entry(existingBooking).CurrentValues.SetValues(performance);


            await Context.SaveChangesAsync();
        }
        public async Task<Performance> Find(string id)
        {
            return await Context.Set<Performance>().FindAsync(id);
        }
        public async Task CreateAsync(Performance performance)
        {
            if (performance == null)
            {
                throw new ArgumentNullException(nameof(performance));
            }
            await Context.Set<Performance>().AddAsync(performance);

            await Context.SaveChangesAsync();
        }
    }
}