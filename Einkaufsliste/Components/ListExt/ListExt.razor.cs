using Einkaufsliste.Components.ListExt;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Components
{
    public class ListExtBase: ComponentBase
    {
        [Inject]
        protected IListService ListService { get; set; }

    }
}
