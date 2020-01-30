using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
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

        public List<Einkauf> Liste {
            get
            {
                return EinkaufSrv.List;
            }
        }
        public string newEinkauf;

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

    }
}
