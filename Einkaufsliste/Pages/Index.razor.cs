using Einkaufsliste.Components.ListExt;
using Einkaufsliste.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Pages
{
    public class IndexBase : ComponentBase, IDisposable
    {
        [Inject]
        public ListServices ListServices { get; set; }
        public bool ShowFavoritenModal { get; set; }

        public ListService ListEinkauf
        {
            get
            {
                return ListServices.ListEinkauf;
            }
        }

        public void ShowFavoriten()
        {
            ShowFavoritenModal = true;
        }

        public async Task Archive()
        {
            await ListServices.ArchivService.Archivieren(ListServices.ListEinkauf.ListItems);
            await ListServices.ListEinkauf.ClearList();
        }
        

        public void CloseFavoriten()
        {
            ShowFavoritenModal = false;
        }

        public async Task InsertFavoriten()
        {

            foreach (var s in ListServices.ListFavoriten.SelectedItems)
            {
                await ListServices.ListEinkauf.AddItem(new ListItem()
                {
                    Title = s
                });
            }
            ListServices.ListFavoriten.SelectedItems.Clear();
            CloseFavoriten();

        }

        protected override async Task OnInitializedAsync()
        {
            await ListServices.LoadSettings();
            ListServices.ListEinkauf.IsFiltered = ListServices.Settings.IsFiltered;
            ListServices.ListEinkauf.IsSortByName = ListServices.Settings.IsSortByName;
        }

        public void Dispose()
        {
            Task.Run(() => ListServices.SaveSettings());
        }
    }
}
