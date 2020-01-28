﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste
{
    public class EinkaufServiceTest : IEinkaufService
    {
        public List<Einkauf> List { get; set; }

        public void AddEinkauf(Einkauf item)
        {

            if (!string.IsNullOrWhiteSpace(item.Name))
            {
                List.Add(item);
                // LocalStorage.SetItemAsync("Current", Liste);
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
    }
}
