using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request.Producto;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.UI.Common;
using Papa.Facturacion.Utils;

namespace Papa.Facturacion.UI.Components.Pages.Features.Products
{
    public partial class CreateProduct
    {
        [Inject]
        private ICatalogoDetalleService _catalogoDetalleService { get; set; } = default!;

        [Inject]
        private IProductoService _service { get; set; } = default!;

        [Inject]
        private NavigationManager _navigation { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        private List<ListCatalogoDetalleByCodigoResponse> ListLaboratorio { get; set; } = new();
        private List<ListCatalogoDetalleByCodigoResponse> ListCategoria { get; set; } = new();
        private List<ListCatalogoDetalleByCodigoResponse> ListMarca { get; set; } = new();
        public ProductoRequest Request { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCatalogoDetalleAsync();
        }

        private async Task GetCatalogoDetalleAsync()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _catalogoDetalleService.ListAsync(new List<string> { CodesCatalog.LABORATORIO, CodesCatalog.CATEGORIA, CodesCatalog.MARCA });

                if (result.IsSuccess && result.Result != null)
                {
                    ListLaboratorio = result.Result.Where(x => x.CodigoPadre == CodesCatalog.LABORATORIO).ToList();
                    ListCategoria = result.Result.Where(x => x.CodigoPadre == CodesCatalog.CATEGORIA).ToList();
                    ListMarca = result.Result.Where(x => x.CodigoPadre == CodesCatalog.MARCA).ToList();
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

        private async Task SaveProduct()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.AddAsync(Request);

                if (result.IsSuccess)
                {
                    Toast.Notify(new(ToastType.Success, "Producto registrado exitosamente"));
                    _navigation.NavigateTo(ComponentRoutes.Products.List);
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