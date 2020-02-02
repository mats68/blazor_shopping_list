using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Services
{
    public class ArchivService
    {
        ILocalStorageService LocalStorage { get; set; }
        public List<string> ArchivList { get; set; }
        public string Key { get; set; } = "archive";

        public ArchivService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
            ArchivList = new List<string>();
        }

        public async Task Load()
        {
            var list = await LocalStorage.GetItemAsync<List<string>>(Key);
            if (list != null)
            {
                ArchivList = list;
            }
        }



    }
}
