using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    interface IEinkaufService
    {
        List<Einkauf> List { get; set; }
        Einkauf CurrentItem { get; set; }
        bool IsSortByName { get; set; }
        Task GetList();
        Task AddEinkauf(Einkauf item);
        Task ToggleIsDone(Einkauf item);
        Task DeleteEinkauf();
        void Sort();
    }
}
