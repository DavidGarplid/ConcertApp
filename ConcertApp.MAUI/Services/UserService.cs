using ConcertApp.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.MAUI.Services
{
    public class UserService : IUserService
    {
        private readonly IRestService<User> _restService;
        public UserService(IRestService<User> restService)
        {
            _restService = restService;
        }


        public async Task<User> GetUserAsync(int id)
        {
            return await _restService.GetByIdAsync(id);
        }
    }
}
