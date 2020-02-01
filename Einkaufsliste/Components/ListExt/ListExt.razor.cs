using Einkaufsliste.Components.ListExt;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Components
{
    public class ListExtBase: ComponentBase
    {
        [Parameter]
        public ListService ListService { get; set; }
        [Parameter]
        public RenderFragment HeaderButtons { get; set; }


        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public List<ListItem> Liste
        {
            get
            {
                return ListService.ListItems;
            }
        }

        public string newItem;
        public ElementReference editNameRef;

        public async Task AddItem()
        {
            await ListService.AddItem(new ListItem() { Title = newItem });
            newItem = string.Empty;
            await Focus(editNameRef);
        }

        public string SortTitle
        {
            get { return ListService.IsSortByName ? "Nach Eintragung sortieren" : "Nach Namen sortieren"; }
        }
        public string FilterTitle
        {
            get { return ListService.IsFiltered ? "Alle anzeigen" : "Erledigte ausblenden"; }
        }

        public void Sort()
        {
            ListService.IsSortByName = !ListService.IsSortByName;
        }

        public void Filter()
        {
            ListService.IsFiltered = !ListService.IsFiltered;
        }

        public async Task Focus(ElementReference elementRef)
        {
            await JSRuntime.InvokeVoidAsync(
                "JsFunctions.focusElement", elementRef);
        }

        protected override async Task OnInitializedAsync()
        {
            await ListService.Load();
        }

    }
}
