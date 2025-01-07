using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertApp.MAUI.Models;


    namespace ConcertApp.MAUI.Services
    {
        public class ConcertAppService<T> : IConcertAppService<T>
        {
            private readonly IRestService<T> _restService;

            public ConcertAppService(IRestService<T> restService)
            {
                _restService = restService;
            }

            public Task<List<T>?> GetItemsAsync()
            {
                return _restService.RefreshDataAsync();
            }

            public Task SaveItemAsync(T item, bool isNewItem)
            {
                return _restService.SaveItemAsync(item, isNewItem);
            }

            public Task DeleteItemAsync(T item)
            {
                // Assuming `T` has an ID property for deletion
                var idProperty = typeof(T).GetProperty("ID");
                if (idProperty != null)
                {
                    var idValue = idProperty.GetValue(item)?.ToString();
                    if (!string.IsNullOrEmpty(idValue))
                    {
                        return _restService.DeleteItemAsync(idValue);
                    }
                }
                throw new ArgumentException("The item does not have a valid ID property.");
            }
        }
    }

