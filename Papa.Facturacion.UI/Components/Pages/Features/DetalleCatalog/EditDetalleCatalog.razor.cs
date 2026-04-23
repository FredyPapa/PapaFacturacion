using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request.CatalogoDetalle;
using Papa.Facturacion.Dto.Request.Cliente;
using Papa.Facturacion.Dto.Response.Catalogo;
using Papa.Facturacion.UI.Common;

namespace Papa.Facturacion.UI.Components.Pages.Features.DetalleCatalog
{
    public partial class EditDetalleCatalog
    {
        [Parameter]
        public int id { get; set; }

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
            await GetByIdAsync();
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

        private async Task GetByIdAsync()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result.IsSuccess && result.Result != null)
                {
                    Request.ICatalogo = result.Result.ICatalogo;
                    Request.VCodigo = result.Result.VCodigo;
                    Request.VDescripcion = result.Result.VDescripcion;
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
                var update = new CatalogoDetalleRequest()
                {
                    ICatalogo = Request.ICatalogo,
                    VCodigo = Request.VCodigo,
                    VDescripcion = Request.VDescripcion,
                };

                var result = await _service.UpdateAsync(id, update);

                if (result.IsSuccess)
                {
                    Toast.Notify(new(ToastType.Success, "Catálogo actualizado exitosamente"));
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
