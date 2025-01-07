using ConcertApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConcertApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ConcertApp.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public BookingRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task UpdateAsync(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            var existingBooking = await Context.Set<Booking>().FindAsync(booking.Id);
            Context.Entry(existingBooking).CurrentValues.SetValues(booking);


            await Context.SaveChangesAsync();
        }

        public async Task<Booking> Find(string id)
        {
            return await DbContext.Bookings.FindAsync(id);
        }
        public void Delete(string id)
        {
            var entity = Context.Set<Booking>().Find(id);
            if (entity == null)
            {
                throw new InvalidOperationException("Entity not found with the given ID.");
            }
            Context.Set<Booking>().Remove(entity);

            Context.SaveChanges();
        }
        public async Task CreateAsync(Booking booking)
        {
            if(booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            await Context.Set<Booking>().AddAsync(booking);

            await Context.SaveChangesAsync();
        }
    }
}
