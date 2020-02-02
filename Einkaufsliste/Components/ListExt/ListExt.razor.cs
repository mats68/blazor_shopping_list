using Einkaufsliste.Components.ListExt;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Components
{
    public class ListExtBase : ComponentBase
    {
        [Parameter]
        public ListService ListService { get; set; }
        [Parameter]
        public RenderFragment HeaderButtons { get; set; }
        [Parameter]
        public bool HideHeader { get; set; }
        [Parameter]
        public bool IsMultiSelect { get; set; }
        [Parameter]
        public bool IsBoldText { get; set; }


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

        public bool IsDeleteMode { get; set; }

        public async Task AddItem()
        {
            await ListService.AddItem(new ListItem() { Title = newItem });
            newItem = string.Empty;
            await Focus(editNameRef);
        }

        public async Task Delete(ListItem item)
        {
            await ListService.DeleteItem(item);
        }

        public void Up()
        {
            if (ListService.CurrentItem != null)
            {
                ListService.Up(ListService.CurrentItem);
            }
        }

        public void Down()
        {
            if (ListService.CurrentItem != null)
            {
                ListService.Down(ListService.CurrentItem);
            }
        }

        public void SetCurrent(ListItem item)
        {
            ListService.CurrentItem = item;
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

        public async Task ToggleIsDone(ListItem item)
        {
            await ListService.ToggleIsDone(item);
        }

        public void ToggleDeleteButtons()
        {
            IsDeleteMode = !IsDeleteMode;
        }

        public string ClassActive(ListItem item)
        {
            return ListService.CurrentItem == item ? "list-group-item list-group-item-action active" : "list-group-item list-group-item-action";
        }

        public string ClassText(ListItem item)
        {
            var s = IsBoldText ? "font-weight-bold" : "font-weight-normal";
            var s1 = item.IsDone ? " line-through" : "";
            return s + s1;
        }

        public string ClassBtnPressed(bool pressed)
        {
            return pressed ? "btn-outline-info" : "btn-info";
        }

        public void ItemSelected(ListItem item, object checkedValue)
        {
            var c = (bool)checkedValue;

            if (c && !ListService.SelectedItems.Contains(item.Title)) ListService.SelectedItems.Add(item.Title);
            if (!c && ListService.SelectedItems.Contains(item.Title)) ListService.SelectedItems.Remove(item.Title);
            
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
