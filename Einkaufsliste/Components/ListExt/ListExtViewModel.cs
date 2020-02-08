using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Einkaufsliste.Components
{
    public class ListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public bool IsCat { get; set; }
        public int CatId { get; set; }
        public bool Exp { get; set; }
    }


    public class ListExtViewModel
    {

        private List<ListItem> listitems;
        private readonly string key;

        private ILocalStorageService LocalStorage { get; }

        public List<ListItem> ListItems
        {
            get
            {
                //Func<int, List<ListItem>> f1 = cat => listitems.Where(i => i.CatId == cat).ToList()
                var query = listitems;
                if (IsSortByName)

                    query = query.OrderBy(i => i.CatId).ThenBy(i => i.Title).ToList();
                else
                    query = query.OrderBy(i => i.CatId).ThenBy(i => i.Id).ToList();
                if (IsFiltered) query = query.Where(i => !i.IsDone).ToList();
                return query.ToList();
            }
        }
        public List<string> SelectedItems { get; set; }

        public bool IsSortByName { get; set; }
        public bool IsFiltered { get; set; }

        public ListItem CurrentItem { get; set; }

        public bool AllItemsDone
        {
            get
            {
                if (listitems.Count() == 0) return false;
                return listitems.Where(i => !i.IsDone).Count() == 0;
            }
        }

        public ListExtViewModel(ILocalStorageService localStorage, string key)
        {
            LocalStorage = localStorage;
            this.key = key;
            listitems = new List<ListItem>();
            SelectedItems = new List<string>();
        }

        public async Task Load()
        {
            var l = await LocalStorage.GetItemAsync<List<ListItem>>(key);
            if (l != null)
            {
                listitems = l;
            }
        }

        public async Task AddItem(ListItem item)
        {
            var found = listitems.Find(i => string.Equals(i.Title.ToLower(), item.Title.ToLower()));
            if (!string.IsNullOrWhiteSpace(item.Title) && found == null)
            {
                var newId = listitems.Count() > 0 ? listitems.Max(e => e.Id) + 1 : 1;
                item.Id = newId;
                var catId = 0;
                if (CurrentItem != null)
                {
                    if (CurrentItem.IsCat)
                    {
                        catId = CurrentItem.Id;
                    }
                    else
                    {
                        catId = CurrentItem.CatId;
                    }
                }
                item.CatId = catId;
                listitems.Add(item);
                await Save();
                CurrentItem = item;
            }
        }

        public async Task ExpandItem(ListItem item)
        {
            item.Exp = !item.Exp;
            await Save();
        }

        public async Task DeleteItem(ListItem item)
        {
            listitems.Remove(item);
            await Save();
        }

        public async Task ToggleIsDone(ListItem item)
        {
            item.IsDone = !item.IsDone;
            await Save();
        }

        public async Task Up(ListItem item)
        {
            var index = listitems.IndexOf(item);
            if (index > 0)
            {
                var id = item.Id;
                var item2 = listitems[index - 1];
                var newId = item2.Id;
                item.Id = newId;
                item2.Id = id;
                //listitems = listitems.OrderBy(e => e.CatId).ThenBy(e => e.Id).ToList();
                await Save();
            }
        }

        public async Task Down(ListItem item)
        {
            var index = listitems.IndexOf(item);
            if (index < (listitems.Count() - 1))
            {
                var id = item.Id;
                var item2 = listitems[index + 1];
                var newId = item2.Id;
                item.Id = newId;
                item2.Id = id;
                //listitems = listitems.OrderBy(e => e.CatId).ThenBy(e => e.Id).ToList();
                await Save();
            }

        }

        public async Task ClearList()
        {
            listitems.Clear();
            await LocalStorage.RemoveItemAsync(key);
            CurrentItem = null;
        }

        private async Task Save()
        {
            await LocalStorage.SetItemAsync(key, listitems);
        }



    }
}
