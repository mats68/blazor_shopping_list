using Blazored.LocalStorage;
using Einkaufsliste.Components.ListExt;
using Einkaufsliste.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Shared
{
    public class ArchivListeBase : ComponentBase
    {
        [Inject]
        public ArchivService ArchivService { get; set; }
        [Inject]
        ILocalStorageService LocalStorage { get; set; }

        public string Current { get; set; }
        public ListService CurrentList { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await ArchivService.Load();
        }


        public void SetCurrent(string item)
        {
            if (Current == item)
            {
                Current = null;
                CurrentList = null;
            }
            else
            {
                Current = item;
                CurrentList = new ListService(LocalStorage, new ListServiceAttrs()
                {
                    Key = Current,
                    ShowIsDoneButton = false
                });
            }

        }
    }
}