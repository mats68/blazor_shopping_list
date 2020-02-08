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
        public ListExtViewModel ListExtViewModel { get; set; }
        [Parameter]
        public string NewItemText { get; set; }
        [Parameter]
        public bool ShowIsDoneButton { get; set; }
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
                return ListExtViewModel.ListItems;
            }
        }

        public string newItem;
        public ElementReference editNameRef;

        public bool IsDeleteMode { get; set; }
        public List<ListItem> GetListe()
        {
            return ListExtViewModel.ListItems.Where(i => i.CatId == 0).ToList();
        }

        public async Task AddItem()
        {
            await ListExtViewModel.AddItem(new ListItem() { Title = newItem });
            newItem = string.Empty;
            await Focus(editNameRef);
        }

        public async Task AddFolder()
        {
            await ListExtViewModel.AddItem(new ListItem() { IsCat = true, Title = newItem });
            newItem = string.Empty;
            await Focus(editNameRef);
        }

        public async Task ExpandFolder(ListItem item)
        {
            await ListExtViewModel.ExpandItem(item);
        }

        public string GetColCount(ListItem item)
        {
            var anz = 10;
            if (IsMultiSelect) anz--;
            if (item.IsCat) anz--;

            return anz.ToString();
        }

        public string IconExpand(ListItem item)
        {
            return item.Exp ? "oi-caret-top" : "oi-caret-bottom";
        }

        public async Task Delete(ListItem item)
        {
            await ListExtViewModel.DeleteItem(item);
        }

        public async Task Up()
        {
            if (ListExtViewModel.CurrentItem != null)
            {
                await ListExtViewModel.Up(ListExtViewModel.CurrentItem);
            }
        }

        public async Task Down()
        {
            if (ListExtViewModel.CurrentItem != null)
            {
                await ListExtViewModel.Down(ListExtViewModel.CurrentItem);
            }
        }

        public void SetCurrent(ListItem item)
        {
            ListExtViewModel.CurrentItem = item;
        }

        public string SortTitle
        {
            get { return ListExtViewModel.IsSortByName ? "Nach Eintragung sortieren" : "Nach Namen sortieren"; }
        }
        public string FilterTitle
        {
            get { return ListExtViewModel.IsFiltered ? "Alle anzeigen" : "Erledigte ausblenden"; }
        }

        public void Sort()
        {
            ListExtViewModel.IsSortByName = !ListExtViewModel.IsSortByName;
        }

        public void Filter()
        {
            ListExtViewModel.IsFiltered = !ListExtViewModel.IsFiltered;
        }

        public async Task ToggleIsDone(ListItem item)
        {
            await ListExtViewModel.ToggleIsDone(item);
        }

        public void ToggleDeleteButtons()
        {
            IsDeleteMode = !IsDeleteMode;
        }

        public string ClassActive(ListItem item)
        {
            return ListExtViewModel.CurrentItem == item ? "list-group-item list-group-item-action active" : "list-group-item list-group-item-action";
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

            if (c && !ListExtViewModel.SelectedItems.Contains(item.Title)) ListExtViewModel.SelectedItems.Add(item.Title);
            if (!c && ListExtViewModel.SelectedItems.Contains(item.Title)) ListExtViewModel.SelectedItems.Remove(item.Title);
            
        }

        public async Task Focus(ElementReference elementRef)
        {
            await JSRuntime.InvokeVoidAsync(
                "JsFunctions.focusElement", elementRef);
        }

        protected override async Task OnInitializedAsync()
        {
            await ListExtViewModel.Load();
        }

    }
}
