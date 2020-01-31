using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Shared
{
    public class ListeBase: ComponentBase
    {


        [Inject]
        IEinkaufService EinkaufSrv { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public List<Einkauf> Liste {
            get
            {
                return EinkaufSrv.List;
            }
        }
        public string newEinkauf;
        public bool IsFavoritenMode { get; set; }
        public ElementReference editNameRef;

        protected override async Task OnInitializedAsync()
        {
            await EinkaufSrv.GetList();
        }

        public Einkauf CurrentItem
        {
            get { return EinkaufSrv.CurrentItem; }
        }

        public string Erledigt(Einkauf item)
        {
            return item.IsDone ? "line-through" : "";
        }


        public async Task AddEinkauf()
        {
            await Task.Run(() => EinkaufSrv.AddEinkauf(new Einkauf { Name = newEinkauf }));
            newEinkauf = string.Empty;
            await Focus(editNameRef);
        }
        public async Task ToggleIsDone(Einkauf item)
        {
            await Task.Run(() => EinkaufSrv.ToggleIsDone(item));
        }
        public async Task DeleteEinkauf()
        {
            await Task.Run(() => EinkaufSrv.DeleteEinkauf());
        }
        
        public string GetSortClass ()
        {
            return EinkaufSrv.IsSortByName ? "btn-outline-info" : "btn-info";
        }

        public string GetSortTitle()
        {
            return EinkaufSrv.IsSortByName ? "Nach Eintragung sortieren" : "Nach Namen sortieren";
        }

        public void Sort()
        {
            EinkaufSrv.Sort();
        }

        public string GetFilterClass()
        {
            return EinkaufSrv.IsFiltered ? "btn-outline-info" : "btn-info";
        }

        public string GetFilterTitle()
        {
            return EinkaufSrv.IsFiltered ? "Alle anzeigen" : "Erledigte ausblenden";
        }

        public void Filter()
        {
            EinkaufSrv.Filter();
        }

        public void ShowFavoriten()
        {
            IsFavoritenMode = !IsFavoritenMode;
        }

        public void CloseFavoriten(MouseEventArgs e)
        {
            IsFavoritenMode = false;
        }

        public async Task Focus(ElementReference elementRef)
        {
            await JSRuntime.InvokeVoidAsync(
                "JsFunctions.focusElement", elementRef);
        }

    }
}
