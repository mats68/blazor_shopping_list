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
            ListEinkauf = new ListService(LocalStorage, 
            new ListServiceAttrs()
            {
                Key = "cur",
                NewItemText = "Was willst du einkaufen ?"
            });

            ListFavoriten = new ListService(LocalStorage,
            new ListServiceAttrs()
            {
                Key = "fav",
                NewItemText = "Neuen Favoriten eingeben"
            });
        }

        ILocalStorageService LocalStorage { get; set; }
        public ListService ListEinkauf { get; set; }
        public ListService ListFavoriten { get; set; }
    }
}
