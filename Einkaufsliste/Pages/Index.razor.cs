﻿using Einkaufsliste.Components.ListExt;
using Einkaufsliste.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Pages
{
    public class IndexBase : ComponentBase
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

        public void CloseFavoriten()
        {
            ShowFavoritenModal = false;
        }

    }
}
