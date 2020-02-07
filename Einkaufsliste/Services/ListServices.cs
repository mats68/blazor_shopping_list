using Blazored.LocalStorage;
using Einkaufsliste.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Services
{

    public class ListServices
    {

        public ListServices(ILocalStorageService localStorage, ArchivService archivService)
        {
            LocalStorage = localStorage;
            ArchivService = archivService;

            ListEinkauf = new ListExtViewModel(LocalStorage, "cur");
            ListFavoriten = new ListExtViewModel(LocalStorage, "fav");
        }

        readonly string settingsKey = "settings";
        ILocalStorageService LocalStorage { get; set; }
        public ArchivService ArchivService { get; }
        public ListExtViewModel ListEinkauf { get; set; }
        public ListExtViewModel ListFavoriten { get; set; }
        public Settings Settings { get; set; }

        public async Task LoadSettings()
        {
            
            Settings = await LocalStorage.GetItemAsync<Settings>(settingsKey);
            if (Settings == null)
            {
                Settings = new Settings();
            }
        }

        public async Task SaveSettings()
        {
            Settings.IsFiltered = ListEinkauf.IsFiltered;
            Settings.IsSortByName = ListEinkauf.IsSortByName;
            await LocalStorage.SetItemAsync(settingsKey,Settings);
        }


    }

}
