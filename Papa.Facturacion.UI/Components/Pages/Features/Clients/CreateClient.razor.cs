using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request.Cliente;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.UI.Common;

namespace Papa.Facturacion.UI.Components.Pages.Features.Clients
{
    public partial class CreateClient
    {
        [Inject]
        private ICatalogoDetalleService _catalogoDetalleService { get; set; } = default!;

        [Inject]
        private IClienteService _service { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        private List<ListCatalogoDetalleByCodigoResponse> ListTipoDoc { get; set; } = new();
        public ClienteRequest Request { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCatalogoDetalleAsync();
        }

        private async Task GetCatalogoDetalleAsync()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _catalogoDetalleService.ListAsync(new List<string> { "MAE_TD" });

                if (result.IsSuccess && result.Result != null)
                {
                    ListTipoDoc = result.Result.Where(x => x.CodigoPadre == "MAE_TD").ToList();
                }
                else
                {
                    Toast.Notify(new(ToastType.Warning, result.Message!));
                }
            }
            catch (Exception ex)
            {
                Toast.Notify(new(ToastType.Danger, ex.Message));
            }
            finally
            {
                PreloadService.Hide();
            }
        }

        private async Task SaveClient()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.AddAsync(Request);

                if (result.IsSuccess)
                {
                    Toast.Notify(new(ToastType.Success, "Cliente registrado exitosamente"));
                    _navigation.NavigateTo(ComponentRoutes.Clients.List);
                }
                else
                {
                    Toast.Notify(new(ToastType.Warning, result.Message!));
                }
            }
            catch (Exception ex)
            {
                Toast.Notify(new(ToastType.Danger, ex.Message));
            }
            finally
            {
                PreloadService.Hide();
            }
        }
    }
}
