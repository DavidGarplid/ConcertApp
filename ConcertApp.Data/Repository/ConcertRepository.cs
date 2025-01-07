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
    public class ConcertRepository : Repository<Concert>, IConcertRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public ConcertRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public async Task UpdateAsync(Concert concert)
        {
            if (concert == null)
            {
                throw new ArgumentNullException(nameof(concert));
            }
            var existingBooking = await Context.Set<Concert>().FindAsync(concert.ID);
            Context.Entry(existingBooking).CurrentValues.SetValues(concert);


            await Context.SaveChangesAsync();
        }

        public async Task<Concert> Find(string id)
        {
            return await Context.Set<Concert>().FindAsync(id);
        }
        public async Task CreateAsync(Concert concert)
        {
            if (concert == null)
            {
                throw new ArgumentNullException(nameof(concert));
            }
            await Context.Set<Concert>().AddAsync(concert);

            await Context.SaveChangesAsync();
        }
    }
}
