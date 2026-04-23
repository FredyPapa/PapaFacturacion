using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request.CatalogoDetalle;
using Papa.Facturacion.Dto.Request.Cliente;
using Papa.Facturacion.Dto.Response.Catalogo;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.UI.Common;
using Papa.Facturacion.Utils;

namespace Papa.Facturacion.UI.Components.Pages.Features.DetalleCatalog
{
    public partial class CreateDetalleCatalog
    {
        [Inject]
        private ICatalogoService _catalogoService { get; set; } = default!;

        [Inject]
        private ICatalogoDetalleService _service { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        private List<ListCatalogoResponse> ListCatalogo { get; set; } = new();
        public CatalogoDetalleRequest Request { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCatalogoAsync();
        }

        private async Task GetCatalogoAsync()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _catalogoService.ListAsync();

                if (result.IsSuccess && result.Result != null)
                {
                    ListCatalogo = result.Result.ToList();
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

        private async Task SaveDetalleCatalog()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.AddAsync(Request);

                if (result.IsSuccess)
                {
                    Toast.Notify(new(ToastType.Success, "Catálogo registrado exitosamente"));
                    _navigation.NavigateTo(ComponentRoutes.DetalleCatalog.List);
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
