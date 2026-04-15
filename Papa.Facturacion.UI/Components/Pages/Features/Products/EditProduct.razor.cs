using BlazorBootstrap;
using Mapster;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request.Producto;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.UI.Common;
using Papa.Facturacion.Utils;

namespace Papa.Facturacion.UI.Components.Pages.Features.Products
{
    public partial class EditProduct
    {
        [Parameter]
        public int id { get; set; }

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
            await GetByIdAsync();
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

        private async Task GetByIdAsync()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.GetByIdAsync(id);

                if (result.IsSuccess && result.Result != null)
                {
                    var product = result.Result;
                    Request = product.Adapt<ProductoRequest>();
                    /*
                    Request.VNombre = result.Result.VNombre;
                    Request.VDescripcion = result.Result.VDescripcion;
                    Request.ILaboratorioCat = result.Result.ILaboratorioCat;
                    Request.ICategoriaCat = result.Result.ICategoriaCat;
                    Request.IMarcaCat = result.Result.IMarcaCat;
                    Request.DcPrecioUnitario = result.Result.DcPrecioUnitario;
                    Request.IStock = result.Result.IStock;
                    */
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
                /*
                var update = new ProductoRequest()
                {
                    VNombre = Request.VNombre,
                    VDescripcion = Request.VDescripcion,
                    ILaboratorioCat = Request.ILaboratorioCat,
                    ICategoriaCat = Request.ICategoriaCat,
                    IMarcaCat = Request.IMarcaCat,
                    DcPrecioUnitario = Request.DcPrecioUnitario,
                    IStock = Request.IStock
                };
                */
                var update = Request.Adapt<ProductoRequest>();

                var result = await _service.UpdateAsync(id, update);

                if (result.IsSuccess)
                {
                    Toast.Notify(new(ToastType.Success, "Producto editado exitosamente"));
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
