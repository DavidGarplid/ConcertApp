using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        IConcertRepository Concerts { get; }
        IPerformanceRepository Performances { get; }
        IUserRepository Users { get; }
        Task<int> Complete();
    }
}