using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Shared
{
    public class ArchivListeBase: ComponentBase
    {
        [Inject]
        IEinkaufService EinkaufSrv { get; set; }

        public List<string> ArchivListe
        {
            get
            {
                return EinkaufSrv.ArchivList;
            }
        }

        public List<Einkauf> CurrentListe
        {
            get
            {
                return EinkaufSrv.List;
            }
        }

        public string CurrentArchiveItem
        {
            get
            {
                return EinkaufSrv.CurrentArchiveItem;
            }
        }

        public List<Einkauf> ArchiveListItems
        {
            get
            {
                return EinkaufSrv.ArchiveListItems;
            }
        }


        protected override async Task OnInitializedAsync()
        {
            await EinkaufSrv.GetArchivList();
        }

        public async Task ArchiveCurrent()
        {
            await EinkaufSrv.ArchiveCurrent();
            StateHasChanged();
        }

        public async Task ShowArchiveListItems(string item)
        {
            await EinkaufSrv.ShowArchiveListItems(item);
            //await Task.Run(() => EinkaufSrv.ToggleIsDone(item));
        }

    }
}
