using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Components
{
    public class ListExtItemsBase : ComponentBase
    {
        [Parameter]
        public List<ListItem> Items { get; set; }
        [Parameter]
        public ListExtViewModel ListExtViewModel { get; set; }
        [Parameter]
        public bool ShowIsDoneButton { get; set; }
        [Parameter]
        public bool IsMultiSelect { get; set; }
        [Parameter]
        public bool IsBoldText { get; set; }
        [Parameter]
        public bool IsDeleteMode { get; set; }

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

        public async Task Delete(ListItem item)
        {
            await ListExtViewModel.DeleteItem(item);
        }

        public void SetCurrent(ListItem item)
        {
            ListExtViewModel.CurrentItem = item;
        }

        public async Task ToggleIsDone(ListItem item)
        {
            await ListExtViewModel.ToggleIsDone(item);
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

        public void ItemSelected(ListItem item, object checkedValue)
        {
            var c = (bool)checkedValue;

            if (c && !ListExtViewModel.SelectedItems.Contains(item.Title)) ListExtViewModel.SelectedItems.Add(item.Title);
            if (!c && ListExtViewModel.SelectedItems.Contains(item.Title)) ListExtViewModel.SelectedItems.Remove(item.Title);
            
        }

        public string IconExpand(ListItem item)
        {
            return item.Exp ? "oi-caret-top" : "oi-caret-bottom";
        }


    }
}
