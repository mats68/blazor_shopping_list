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

        public async Task AddEinkauf(Einkauf item)
        {

            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                await Task.Run(() => List.Add(item));
                CurrentItem = item;
            }

        }

        public async Task GetList()
        {
            var list = await Task.FromResult<List<Einkauf>>(
                new List<Einkauf>()
                {
                    new Einkauf(){Name = "Radieschen"},
                    new Einkauf(){Name = "Brot"},
                    new Einkauf(){Name = "Käse"},
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

    }
}
