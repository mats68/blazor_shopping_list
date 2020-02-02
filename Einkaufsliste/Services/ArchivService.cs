using Blazored.LocalStorage;
using Einkaufsliste.Components.ListExt;
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
        private bool loaded = false;
        private readonly int MaxEntries = 3;

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
            loaded = true;
        }

        public async Task Archivieren(List<ListItem> list)
        {
            if (!loaded) await Load();
            if (ArchivList.Count() >= MaxEntries)
            {
                var ind = MaxEntries - 1;
                for (int i = ind; i >= ind; i--)
                {
                    await LocalStorage.RemoveItemAsync(ArchivList[i]);
                    ArchivList.RemoveAt(i);
                }
            }
            var dateStr = DateTime.Now.ToString("G");
            ArchivList.Insert(0, dateStr);
            await LocalStorage.SetItemAsync(Key, ArchivList);
            await LocalStorage.SetItemAsync(dateStr, list);
        }


    }
}
