using BlazorBootstrap;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;

namespace Papa.Facturacion.UI.Components.Pages.Features.DetalleCatalog
{
    public partial class ListDetalleCatalog
    {
        [Inject]
        public ICatalogoDetalleService _service { get; set; } = default!;

        [Inject]
        private SweetAlertService Swal { get; set; } = default!;

        [Inject]
        private ToastService Toast { get; set; } = default!;

        [Inject]
        protected PreloadService PreloadService { get; set; } = default!;

        public SearchListRequest Request { get; set; } = new();

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        public ICollection<ListCatalogoDetalleResponse> Response { get; set; } = new List<ListCatalogoDetalleResponse>();

        private PagerRequest Pager { get; set; } = new();

        [Inject]
        private IJSRuntime _js { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await ListDetalleCatalogues();
        }

        private async Task Refresh()
        {
            Request = new();
            await ListDetalleCatalogues();
        }

        private async Task ListDetalleCatalogues()
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
            await ListDetalleCatalogues();
        }

        private async Task Delete(ListCatalogoDetalleResponse item)
        {
            try
            {
                var result = await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Eliminar catálogo",
                    Text = $"Está a punto de eliminar a {item.Descripcion}, no podrá recuperar el registro.",
                    Icon = SweetAlertIcon.Question,
                    ShowCancelButton = true,
                    ConfirmButtonText = "Sí, eliminar",
                    CancelButtonText = "Cancelar"
                });

                if (result.IsConfirmed)
                {
                    PreloadService.Show(SpinnerColor.Light);
                    var resultDelete = await _service.DeleteAsync(item.Id);
                    if (resultDelete.IsSuccess)
                    {
                        await ListDetalleCatalogues();
                        PreloadService.Hide();
                        await Swal.FireAsync("Eliminado", $"El catálogo {item.Descripcion} fue eliminado exitosamente.", SweetAlertIcon.Success);
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

        private async Task ToEdit(int id) => Navigation.NavigateTo($"{Common.ComponentRoutes.DetalleCatalog.Edit}/{id}");

        private async Task ExportExcel()
        {
            PreloadService.Show(SpinnerColor.Light);
            try
            {
                var result = await _service.ExportListAsync(Request);
                var content = result.Result;
                await _js.InvokeVoidAsync("descargarArchivo", "DetalleCatalogo.xlsx", content!.ToArray());
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
