using BlazorBootstrap;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Producto;

namespace Papa.Facturacion.UI.Components.Pages.Features.Products
{
    public partial class ListProduct
    {
        [Inject]
        public IProductoService _service { get; set; } = default!;

        [Inject]
        private SweetAlertService Swal { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        public SearchListRequest Request { get; set; } = new();

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        public ICollection<ListProductoResponse> Response { get; set; } = new List<ListProductoResponse>();

        private PagerRequest Pager { get; set; } = new();

        [Inject]
        private IJSRuntime _js { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await ListProducts();
        }

        private async Task Refresh()
        {
            Request = new();
            await ListProducts();
        }

        private async Task ListProducts()
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
            await ListProducts();
        }

        private async Task Delete(ListProductoResponse item)
        {
            try
            {
                var result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Eliminar producto",
                    Text = $"Está a punto de eliminar {item.Nombre}, no podrá recuperar el registro.",
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
                        await ListProducts();
                        PreloadService.Hide();
                        await Swal.FireAsync("Eliminado", $"El producto {item.Nombre} fue eliminado exitosamente.", SweetAlertIcon.Success);
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

        private async Task ToEdit(int id) => Navigation.NavigateTo($"{Common.ComponentRoutes.Products.Edit}/{id}");

        private async Task ExportExcel()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.ExportListAsync(Request);
                var content = result.Result;
                await _js.InvokeVoidAsync("descargarArchivo", "Productos.xlsx", content!.ToArray());
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

    }
}
