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
        IEinkaufService EinkaufSrv { get; set; }

        public List<Einkauf> Liste { get; set; }
        public string newEinkauf;

        public Einkauf CurrentItem
        {
            get { return EinkaufSrv.CurrentItem; }
        }

        public string Erledigt(Einkauf item)
        {
            return item.IsDone ? "line-through" : "";
        }


        protected override async Task OnInitializedAsync()
        {
            await EinkaufSrv.GetList();
            Liste = EinkaufSrv.List;
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

    }
}
