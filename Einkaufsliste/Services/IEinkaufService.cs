using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    interface IEinkaufService
    {
        List<Einkauf> List { get; }
        List<string> ArchivList { get; set; }
        Einkauf CurrentItem { get; set; }
        string CurrentArchiveItem { get; set; }
        List<Einkauf> ArchiveListItems { get; set; }
        bool IsSortByName { get; set; }
        bool IsFiltered { get; set; }
        Task GetList();
        Task AddEinkauf(Einkauf item);
        Task ToggleIsDone(Einkauf item);
        Task DeleteEinkauf();
        void Sort();
        void Filter();
        Task GetArchivList();
        Task ArchiveCurrent();
        Task ShowArchiveListItems(string item);
    }
}
