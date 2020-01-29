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

        protected override async Task OnInitializedAsync()
        {
            await EinkaufSrv.GetArchivList();
        }

        public async Task ArchiveCurrent()
        {
            await EinkaufSrv.ArchiveCurrent();
            StateHasChanged();
        }

    }
}
