using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertApp.MAUI.Models;

namespace ConcertApp.MAUI.Services
{
    public interface IRestService<T>
    {
        Task<List<T>?> RefreshDataAsync();
        Task SaveItemAsync(T item, bool isNewItem);
        Task DeleteItemAsync(string id);



    }
}
