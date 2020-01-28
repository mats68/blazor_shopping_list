using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    public class EinkaufService : IEinkaufService
    {
        ILocalStorageService LocalStorage { get; set; }

        public List<Einkauf> List { get; set; }
        public Einkauf CurrentItem { get; set; }
        public bool IsSortByName { get ; set ; }

        public EinkaufService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public async Task AddEinkauf(Einkauf item)
        {
            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                var newId = List.Count() > 0 ? List.Max(e => e.Id) + 1 : 1;
                item.Id = newId;
                List.Add(item);
                await Save();
                CurrentItem = item;
            }
        }

        public async Task GetList()
        {
            var list = await LocalStorage.GetItemAsync<List<Einkauf>>("current");
            if (list == null)
            {
                list = new List<Einkauf>();
            }
            List = list;
        }

        public async Task ToggleIsDone(Einkauf item)
        {
            item.IsDone = !item.IsDone;
            await Save();
            CurrentItem = item;

        }

        private async Task Save()
        {
            await LocalStorage.SetItemAsync("current", List);
        }

        public async Task DeleteEinkauf()
        {
            if (CurrentItem != null)
            {
                List.Remove(CurrentItem);
                await Save();
                CurrentItem = null;
            }
        }

        public void Sort()
        {
            IsSortByName = !IsSortByName;
            if (IsSortByName)
            {
                List = List.OrderBy(e => e.Name).ToList();
            } else
            {
                List = List.OrderBy(e => e.Id).ToList();
            }

        }

    }
}
