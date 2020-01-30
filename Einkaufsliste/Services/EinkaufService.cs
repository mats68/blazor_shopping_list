using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    public class EinkaufService : IEinkaufService
    {
        private const string ArchiveKey = "archive";
        private const string CurrentKey = "current";
        private const string FavoritenKey = "favoriten";


        ILocalStorageService LocalStorage { get; set; }

        List<Einkauf> _List { get; set; }


        public List<Einkauf> List
        {
            get
            {
                if (IsFiltered)
                {
                    return _List.Where(e => !e.IsDone).ToList();
                }
                else
                {
                    return _List;
                }
            }
        }


        public List<string> ArchivList { get; set; }
        public Einkauf CurrentItem { get; set; }
        public Einkauf CurrentFavorit { get; set; }
        public bool IsSortByName { get; set; }
        public bool IsFiltered { get; set; }
        public string CurrentArchiveItem { get; set; }
        public List<Einkauf> ArchiveListItems { get; set; }
        public List<Einkauf> Favoriten { get; set; }

        public EinkaufService(ILocalStorageService localStorage)
        {
            _List = new List<Einkauf>();
            LocalStorage = localStorage;
        }

        public async Task GetList()
        {
            var list = await LocalStorage.GetItemAsync<List<Einkauf>>(CurrentKey);
            if (list == null)
            {
                list = new List<Einkauf>();
            }
            _List = list;
        }

        public async Task GetFavoriten()
        {
            var list = await LocalStorage.GetItemAsync<List<Einkauf>>(FavoritenKey);
            if (list == null)
            {
                list = new List<Einkauf>();
            }
            Favoriten = list;
        }

        public async Task AddEinkauf(Einkauf item)
        {
            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                var newId = _List.Count() > 0 ? _List.Max(e => e.Id) + 1 : 1;
                item.Id = newId;
                _List.Add(item);
                await Save();
                CurrentItem = item;
            }
        }


        public async Task ToggleIsDone(Einkauf item)
        {
            item.IsDone = !item.IsDone;
            await Save();
            CurrentItem = item;

        }

        private async Task Save()
        {
            await LocalStorage.SetItemAsync(CurrentKey, _List);
        }

        private async Task SaveFavoriten()
        {
            await LocalStorage.SetItemAsync(FavoritenKey, Favoriten);
        }

        public async Task DeleteEinkauf()
        {
            if (CurrentItem != null)
            {
                _List.Remove(CurrentItem);
                await Save();
                CurrentItem = null;
            }
        }

        public void Sort()
        {
            IsSortByName = !IsSortByName;
            if (IsSortByName)
            {
                _List = _List.OrderBy(e => e.Name).ToList();
            }
            else
            {
                _List = _List.OrderBy(e => e.Id).ToList();
            }

        }

        public void Filter()
        {
            IsFiltered = !IsFiltered;
        }

        public async Task GetArchivList()
        {
            var list = await LocalStorage.GetItemAsync<List<string>>(ArchiveKey);
            if (list == null)
            {
                list = new List<string>();
            }
            ArchivList = list;
        }

        public async Task ArchiveCurrent()
        {
            if (_List.Count() > 0)
            {
                var nameslist = await LocalStorage.GetItemAsync<List<string>>(ArchiveKey);
                if (nameslist == null)
                {
                    nameslist = new List<string>();
                }

                var dateStr = DateTime.Now.ToString("G");
                nameslist.Insert(0, dateStr);

                await LocalStorage.SetItemAsync(ArchiveKey, nameslist);
                await LocalStorage.SetItemAsync(dateStr, _List);
                await LocalStorage.RemoveItemAsync(CurrentKey);
                await GetArchivList();
                _List = null;
                CurrentItem = null;
            }
        }

        public async Task ShowArchiveListItems(string item)
        {
            if (item == CurrentArchiveItem)
            {
                await Task.Run(() =>
                {
                    ArchiveListItems = new List<Einkauf>();
                    CurrentArchiveItem = "";
                });
            }
            else
            {
                var list = await LocalStorage.GetItemAsync<List<Einkauf>>(item);
                if (list == null)
                {
                    list = new List<Einkauf>();
                }
                ArchiveListItems = list;
                CurrentArchiveItem = item;
            }



        }

        public async Task AddFavorit(Einkauf item)
        {
            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                var newId = Favoriten.Count() > 0 ? Favoriten.Max(e => e.Id) + 1 : 1;
                item.Id = newId;
                Favoriten.Add(item);
                await SaveFavoriten();
                CurrentItem = item;
            }
        }
        public void Up(List<Einkauf> list, Einkauf item)
        {
            var index = list.IndexOf(item);
            if (index > 0)
            {
                var id = item.Id;
                var item2 = list[index - 1];
                var newId = item2.Id;
                item.Id = newId;
                item2.Id = id;
                Favoriten = Favoriten.OrderBy(e => e.Id).ToList();
            }
        }

        public void Down(List<Einkauf> list, Einkauf item)
        {
            var index = list.IndexOf(item);
            if (index < (list.Count() - 1))
            {
                var id = item.Id;
                var item2 = list[index + 1];
                var newId = item2.Id;
                item.Id = newId;
                item2.Id = id;
                Favoriten = Favoriten.OrderBy(e => e.Id).ToList();
            }

        }

        public async Task DeleteFavorit(Einkauf item)
        {
            if (item != null)
            {
                Favoriten.Remove(item);
                await SaveFavoriten();
            }
        }

    }
}
