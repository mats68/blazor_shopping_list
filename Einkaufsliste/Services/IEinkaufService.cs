using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    interface IEinkaufService
    {
        Task<List<Einkauf>> GetList();
    }
}
