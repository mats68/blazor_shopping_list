using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Shared
{
    public class FavoritenModalBase: ComponentBase
    {
        public void ClosePopup()
        {

        }

        [Parameter]
        public EventCallback<MouseEventArgs> OnCloseClick { get; set; }
    }
}
