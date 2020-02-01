using Blazored.LocalStorage;
using Einkaufsliste.Components.ListExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Services
{
    public class ListServices
    {
        public ListServices(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
            ListEinkauf = new ListService(LocalStorage);
            ListFavoriten = new ListService(LocalStorage);
        }

        ILocalStorageService LocalStorage { get; set; }
        public ListService ListEinkauf { get; set; }
        public ListService ListFavoriten { get; set;  }
    }
}
