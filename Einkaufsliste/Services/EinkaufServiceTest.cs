using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    public class EinkaufServiceTest : IEinkaufService
    {
        public List<Einkauf> List { get; set; }
        public Einkauf CurrentItem { get; set; }
        public bool IsSortByName { get; set; }

        public async Task AddEinkauf(Einkauf item)
        {

            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                var newId = List.Count() > 0 ? List.Max(e => e.Id) + 1 : 1;
                item.Id = newId;
                await Task.Run(() => List.Add(item));
                CurrentItem = item;
            }

        }

        public async Task GetList()
        {
            var list = await Task.FromResult<List<Einkauf>>(
                new List<Einkauf>()
                {
                    new Einkauf(){Id = 1, Name = "Radieschen"},
                    new Einkauf(){Id = 2, Name = "Brot"},
                    new Einkauf(){Id = 3, Name = "Käse"},
                });    
            List = list;
        }

        public async Task ToggleIsDone(Einkauf item)
        {
            await Task.Run(() => item.IsDone = !item.IsDone);
            CurrentItem = item;
        }

        public async Task DeleteEinkauf()
        {
            await Task.Run(() =>
            {
                if (CurrentItem != null)
                {
                    List.Remove(CurrentItem);
                    CurrentItem = null;
                }
            });
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
    }
}
