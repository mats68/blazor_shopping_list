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

        ILocalStorageService LocalStorage { get; set; }

        public List<Einkauf> List { get; set; }
        public List<string> ArchivList { get; set; }
        public Einkauf CurrentItem { get; set; }
        public bool IsSortByName { get; set; }
        public string CurrentArchiveItem { get ; set; }
        public List<Einkauf> ArchiveListItems { get ; set; }

        public EinkaufService(ILocalStorageService localStorage)
        {
            LocalStorage = localStorage;
        }

        public async Task AddEinkauf(Einkauf item)
        {
            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                var newId = List.Count() > 0 ? List.Max(e => e.Id) + 1 : 1;
                item.Id = newId;
                List.Add(item);
                await Save();
                CurrentItem = item;
            }
        }

        public async Task GetList()
        {
            var list = await LocalStorage.GetItemAsync<List<Einkauf>>(CurrentKey);
            if (list == null)
            {
                list = new List<Einkauf>();
            }
            List = list;
        }

        public async Task ToggleIsDone(Einkauf item)
        {
            item.IsDone = !item.IsDone;
            await Save();
            CurrentItem = item;

        }

        private async Task Save()
        {
            await LocalStorage.SetItemAsync(CurrentKey, List);
        }

        public async Task DeleteEinkauf()
        {
            if (CurrentItem != null)
            {
                List.Remove(CurrentItem);
                await Save();
                CurrentItem = null;
            }
        }

        public void Sort()
        {
            IsSortByName = !IsSortByName;
            if (IsSortByName)
            {
                List = List.OrderBy(e => e.Name).ToList();
            }
            else
            {
                List = List.OrderBy(e => e.Id).ToList();
            }

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
            if (List.Count() > 0)
            {
                var nameslist = await LocalStorage.GetItemAsync<List<string>>(ArchiveKey);
                if (nameslist == null)
                {
                    nameslist = new List<string>();
                }

                var dateStr = DateTime.Now.ToString("G");
                nameslist.Insert(0,dateStr);

                await LocalStorage.SetItemAsync(ArchiveKey, nameslist);
                await LocalStorage.SetItemAsync(dateStr, List);
                await LocalStorage.RemoveItemAsync(CurrentKey);
                await GetArchivList();
                List = null;
                CurrentItem = null;
            }
        }

        public async Task ShowArchiveListItems(string item)
        {
            if (item == CurrentArchiveItem) {
                await Task.Run(() =>
                {
                    ArchiveListItems = new List<Einkauf>();
                    CurrentArchiveItem = "";
                });
            } else
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
    }
}
