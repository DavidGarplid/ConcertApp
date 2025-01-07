using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Booking { get; }
        IConcertRepository Concert { get; }
        IPerformanceRepository Performance { get; }
        IUserRepository User { get; }
        Task<int> Complete();
    }
}