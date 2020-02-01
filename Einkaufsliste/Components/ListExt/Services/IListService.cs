using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Components.ListExt
{
    public interface IListService
    {
        List<ListItem> ListItems { get; }
        bool IsSortByName { get; set; }
        bool IsFiltered { get; set; }
        Task Load();


    }
}
