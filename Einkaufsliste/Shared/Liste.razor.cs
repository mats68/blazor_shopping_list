﻿using Blazored.LocalStorage;
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
        IEinkaufService EinkaufService { get; set; }

        public List<Einkauf> Liste { get; set; }
        public string newEinkauf;

        
        public string Erledigt(Einkauf item)
        {
            return item.IsDone ? "line-through" : "";
        }


        protected override async Task OnInitializedAsync()
        {
            await EinkaufService.GetList();
            Liste = EinkaufService.List;
        }

        public async Task AddEinkauf()
        {
            await Task.Run(() => EinkaufService.AddEinkauf(new Einkauf { Name = newEinkauf }));
            newEinkauf = string.Empty;
        }
        public async Task ToggleIsDone(Einkauf item)
        {
            await Task.Run(() => EinkaufService.ToggleIsDone(item));
        }

    }
}