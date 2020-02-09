using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Einkaufsliste.Components
{
    public class ModalBase: ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public EventCallback<MouseEventArgs> OnCloseClick { get; set; }
        [Parameter]
        public RenderFragment ModalHeader { get; set; }
        [Parameter]
        public RenderFragment ModalBody { get; set; }

    }
}
