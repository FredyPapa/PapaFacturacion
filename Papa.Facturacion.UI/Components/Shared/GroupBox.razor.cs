using Microsoft.AspNetCore.Components;

namespace Papa.Facturacion.UI.Components.Shared
{
    public partial class GroupBox
    {
        [Parameter]
        public string Title { get; set; } = default!;

        [Parameter]
        public RenderFragment Content { get; set; } = default!;

        [Parameter]
        public string Class { get; set; } = default!;
    }
}
