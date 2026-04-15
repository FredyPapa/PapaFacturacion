using BlazorBootstrap;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Cliente;

namespace Papa.Facturacion.UI.Components.Pages.Features.Clients
{
    public partial class ListClient
    {
        [Inject]
        public IClienteService _service { get; set; } = default!;

        [Inject]
        private SweetAlertService Swal { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        public SearchListRequest Request { get; set; } = new();

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        public ICollection<ListClienteResponse> Response { get; set; } = new List<ListClienteResponse>();

        private PagerRequest Pager { get; set; } = new();

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

        private async Task Delete(ListClienteResponse item)
        {
            try
            {
                var result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Eliminar cliente",
                    Text = $"Está a punto de eliminar a {item.Nombres} {item.ApellidoPaterno} {item.ApellidoMaterno}, no podrá recuperar el registro.",
                    Icon = SweetAlertIcon.Question,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Sí, eliminar",
                    CancelButtonText = "Cancelar"
                });

                if(result.IsConfirmed)
                {
                    PreloadService.Show(SpinnerColor.Light);
                    var resultDelete = await _service.DeleteAsync(item.Id);
                    if (resultDelete.IsSuccess)
                    {
                        await ListClients();
                        PreloadService.Hide();
                        await Swal.FireAsync("Eliminado", $"El cliente {item.Nombres} {item.ApellidoPaterno} {item.ApellidoMaterno} fue eliminado exitosamente.", SweetAlertIcon.Success);
                    }
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

        private async Task ToEdit(int id) => Navigation.NavigateTo($"{Common.ComponentRoutes.Clients.Edit}/{id}");

    }
}
