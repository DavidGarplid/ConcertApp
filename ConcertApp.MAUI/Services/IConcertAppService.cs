using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertApp.MAUI.Models;

namespace ConcertApp.MAUI.Services
{
    public interface IConcertAppService<T>
    {
        Task<List<T>?> GetItemsAsync();
        Task SaveItemAsync(T item, bool isNewItem);
        Task DeleteItemAsync(T item);
    }
}
