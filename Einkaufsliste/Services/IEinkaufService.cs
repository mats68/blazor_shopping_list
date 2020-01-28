using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    interface IEinkaufService
    {
        List<Einkauf> List { get; set; }
        Task GetList();
        void AddEinkauf(Einkauf item);
    }
}
