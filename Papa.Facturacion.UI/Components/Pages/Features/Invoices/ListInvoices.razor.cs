using BlazorBootstrap;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Response.Comprobante;

namespace Papa.Facturacion.UI.Components.Pages.Features.Invoices
{
    public partial class ListInvoices
    {
        [Inject]
        private IComprobanteService _service { get; set; } = default!;

        [Inject]
        private SweetAlertService Swal { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        private bool IsLoading { get; set; } = default!;

        public SearchListRequest Request { get; set; } = new();

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        public ICollection<ListComprobanteResponse> Response { get; set; } = new List<ListComprobanteResponse>();

        private PagerRequest Pager { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await ListComprobantes();
        }

        private async Task Refresh()
        {
            Request = new();
            await ListComprobantes();
        }

        private async Task ListComprobantes()
        {
            IsLoading = true;
            try
            {
                var result = await _service.ListAsync(Request);
                if (result!.IsSuccess)
                {
                    Response = result.Result;
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
                IsLoading = false;
            }
        }

        private async Task OnPager()
        {
            Request.Page = Pager.CurrentPage;
            Request.Rows = Pager.RowsPerPage;
            await ListComprobantes();
        }
    }
}
