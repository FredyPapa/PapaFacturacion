using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Implementations;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request.Comprobante;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.Dto.Response.Cliente;
using Papa.Facturacion.Dto.Response.Producto;
using Papa.Facturacion.UI.Common;
using Papa.Facturacion.Utils;

namespace Papa.Facturacion.UI.Components.Pages.Features.Invoices
{
    public partial class CreateInvoices
    {
        [Inject]
        private ICatalogoDetalleService _catalogoDetalleService { get; set; } = default!;

        [Inject]
        private IComprobanteService _service { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        private Modal ModalClient { get; set; } = default!;
        private Modal ModalProduct { get; set; } = default!;

        private ComprobanteRequest Request { get; set; } = new();
        private List<ComprobanteDetalleRequest> RequestProducts { get; set; } = new();

        private decimal TotalBruto => RequestProducts.Sum(x => x.DcTotal);
        //private decimal IGV => TotalBruto * Constants.IGV;
        private decimal IGV => (Request.ITipoComprobanteCat == 13) ? TotalBruto * Constants.IGV : 0;
        private decimal TotalNeto => TotalBruto + IGV;

        private List<ListCatalogoDetalleByCodigoResponse> ListTipoComprobante { get; set; } = new();
        private List<ListCatalogoDetalleByCodigoResponse> ListTipoPago { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCatalogoDetalleAsync();
        }

        private async Task GetCatalogoDetalleAsync()
        {
            PreloadService.Show();
            try
            {
                var result = await _catalogoDetalleService.ListAsync(new List<string> { CodesCatalog.TIPO_COMPROBANTE, CodesCatalog.TIPO_PAGO });

                if (result.IsSuccess && result.Result != null)
                {
                    ListTipoComprobante = result.Result.Where(x => x.CodigoPadre == CodesCatalog.TIPO_COMPROBANTE).ToList();
                    ListTipoPago = result.Result.Where(x => x.CodigoPadre == CodesCatalog.TIPO_PAGO).ToList();
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

        private async Task OnSelectClient(ListClienteResponse item)
        {
            Request.ICliente = item.Id;
            Request.NombreCliente = item.Nombres + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno;
            await ModalClient.HideAsync();
        }

        private async Task OnSelectProduct(ListProductoResponse item)
        {
            var exists = RequestProducts.Any(x => x.IProducto == item.Id);

            if (!exists)
                RequestProducts.Add(new()
                {
                    IProducto = item.Id,
                    NombreProducto = item.Nombre,
                    Marca = item.Marca,
                    DcPrecioUnitario = item.PrecioUnitario,
                });
        }


        private async Task OnSave()
        {
            PreloadService.Show();
            try
            {
                // Limpiamos antes de asignar para evitar duplicados en reintentos
                Request.ComprobanteDetalles = new List<ComprobanteDetalleRequest>();
                Request.ComprobanteDetalles.AddRange(RequestProducts);
                // Asignamos los totales calculados de la UI al objeto que se envía al API
                Request.DcTotalBruto = TotalBruto;
                Request.DcIgv = IGV;
                Request.DcTotaNeto = TotalNeto;
                //
                var result = await _service.AddAsync(Request);
                if (result.IsSuccess)
                {
                    Toast.Notify(new(ToastType.Success, "El comprobante fue registrado exitosamente"));
                    Navigation.NavigateTo(ComponentRoutes.Invoices.List);
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

        private async Task OnRemoveProduct(ComprobanteDetalleRequest item) => RequestProducts.Remove(item);
    }
}
