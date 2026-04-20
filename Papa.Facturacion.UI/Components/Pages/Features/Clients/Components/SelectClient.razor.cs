using BlazorBootstrap;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Response.Cliente;

namespace Papa.Facturacion.UI.Components.Pages.Features.Clients.Components
{
    public partial class SelectClient
    {
        [Inject]
        public IClienteService _service { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        public SearchListRequest Request { get; set; } = new();

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        public ICollection<ListClienteResponse> Response { get; set; } = new List<ListClienteResponse>();

        private PagerRequest Pager { get; set; } = new();

        [Parameter]
        public EventCallback<ListClienteResponse> SelectEvent { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await ListClients();
        }

        private async Task Refresh()
        {
            Request = new();
            await ListClients();
        }

        private async Task ListClients()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.ListAsync(Request);
                if (result!.IsSuccess)
                {
                    Response = result.Result;
                    //
                    Pager = new()
                    {
                        CurrentPage = Request.Page,
                        TotalPages = result.TotalPages,
                        TotalRows = result.TotalRowPerPages,
                        RowsPerPage = Request.Rows
                    };
                }
                else
                {
                    Toast.Notify(new(ToastType.Warning, result.Message!));
                }
            }
            catch (Exception ex)
            {

                Toast.Notify(new(ToastType.Danger, ex.Message!));
            }
            finally
            {
                PreloadService.Hide();
            }
        }

        private async Task OnPager()
        {
            Request.Page = Pager.CurrentPage;
            Request.Rows = Pager.RowsPerPage;
            await ListClients();
        }

        private async Task OnSelect(ListClienteResponse item)
        {
            await SelectEvent.InvokeAsync(item);
        }
    }
}
