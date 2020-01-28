using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    public class EinkaufServiceTest : IEinkaufService
    {
        public List<Einkauf> GetList()
        {
            return new List<Einkauf>()
            {
                new Einkauf(){Name = "Radieschen"},
                new Einkauf(){Name = "Brot"},
                new Einkauf(){Name = "Käse"},
            };

        }
    }
}
