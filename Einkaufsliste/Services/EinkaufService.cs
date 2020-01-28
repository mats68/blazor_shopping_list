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

        public EinkaufService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public void AddEinkauf(Einkauf item)
        {
            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                List.Add(item);
                LocalStorage.SetItemAsync("current", List);
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
    }
}
