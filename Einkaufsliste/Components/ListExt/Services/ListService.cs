using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Einkaufsliste.Components.ListExt
{


    public class ListService 
    {

        private List<ListItem> listitems;

        private ILocalStorageService LocalStorage { get; }
        public ListServiceAttrs ListServiceAttrs { get; }

        public List<ListItem> ListItems
        {
            get
            {
                if (!IsSortByName && !IsFiltered) return listitems;
                var query = listitems;
                if (IsSortByName) query = query.OrderBy(i => i.Title).ToList();
                if (IsFiltered) query = query.Where(i => !i.IsDone).ToList();
                return query.ToList();
            }
        }

        public bool IsSortByName { get; set; }
        public bool IsFiltered { get; set; }

        public ListItem CurrentItem { get; set; }

        public ListService(ILocalStorageService localStorage, ListServiceAttrs listServiceAttrs)
        {
            LocalStorage = localStorage;
            ListServiceAttrs = listServiceAttrs;
            listitems = new List<ListItem>();
        }

        public async Task Load()
        {
            var l = await LocalStorage.GetItemAsync<List<ListItem>>(ListServiceAttrs.Key);
            if (l != null)
            {
                listitems = l;
            }
        }

        public async Task AddItem(ListItem item)
        {
            if (!string.IsNullOrWhiteSpace(item.Title))
            {
                var newId = listitems.Count() > 0 ? listitems.Max(e => e.Id) + 1 : 1;
                item.Id = newId;
                listitems.Add(item);
                await Save();
                CurrentItem = item;
            }
        }

        public async Task DeleteItem()
        {
            if (CurrentItem != null)
            {
                listitems.Remove(CurrentItem);
                await Save();
                CurrentItem = null;
            }
        }

        public async Task ToggleIsDone(ListItem item)
        {
            item.IsDone = !item.IsDone;
            await Save();
            CurrentItem = item;

        }

        public void Up(ListItem item)
        {
            var index = listitems.IndexOf(item);
            if (index > 0)
            {
                var id = item.Id;
                var item2 = listitems[index - 1];
                var newId = item2.Id;
                item.Id = newId;
                item2.Id = id;
                listitems = listitems.OrderBy(e => e.Id).ToList();
            }
        }

        public void Down(ListItem item)
        {
            var index = listitems.IndexOf(item);
            if (index < (listitems.Count() - 1))
            {
                var id = item.Id;
                var item2 = listitems[index + 1];
                var newId = item2.Id;
                item.Id = newId;
                item2.Id = id;
                listitems = listitems.OrderBy(e => e.Id).ToList();
            }

        }



        private async Task Save()
        {
            await LocalStorage.SetItemAsync(ListServiceAttrs.Key, listitems);
        }



    }
}
