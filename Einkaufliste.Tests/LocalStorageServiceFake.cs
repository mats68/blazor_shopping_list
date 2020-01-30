using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace Einkaufsliste.Tests
{
    class LocalStorageServiceFake : ILocalStorageService
    {
        public event EventHandler<ChangingEventArgs> Changing;
        public event EventHandler<ChangedEventArgs> Changed;

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ContainKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetItemAsync<T>(string key)
        {
            return Task<T>.Run(() => 0);
        }

        public Task<string> KeyAsync(int index)
        {
            return Task.Run(() => "");

        }

        public Task<int> LengthAsync()
        {
            return Task.Run(() => 0);

        }

        public Task RemoveItemAsync(string key)
        {
            return Task.Run(() => { });
        }

        public Task SetItemAsync(string key, object data)
        {
            return Task.Run(() => { });
        }

        Task<T> ILocalStorageService.GetItemAsync<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}
