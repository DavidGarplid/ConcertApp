using ConcertApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IRestService<Performance> _restService;
        public PerformanceService(IRestService<Performance> restService)
        {
            _restService = restService;
        }


    }
}
