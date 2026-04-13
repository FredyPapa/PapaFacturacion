using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request.Cliente;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.UI.Common;

namespace Papa.Facturacion.UI.Components.Pages.Maintenance.Clients
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
        private IToastService Toast { get; set; } = default!;

        private List<ListCatalogoDetalleByCodigoResponse> ListTipoDoc { get; set; } = new();
        public ClienteRequest Request { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await GetCatalogoDetalleAsync();
        }

        private async Task GetCatalogoDetalleAsync()
        {
            try
            {
                var result = await _catalogoDetalleService.ListAsync(new List<string> { "MAE_TD" });

                if (result.IsSuccess && result.Result != null)
                {
                    ListTipoDoc = result.Result.Where(x => x.CodigoPadre == "MAE_TD").ToList();
                }
                else
                {
                    Toast.ShowError(result.Message);
                }
            }
            catch (Exception ex)
            {
                Toast.ShowError(ex.Message);
            }
        }

        private async Task SaveClient()
        {
            var result = await _service.AddAsync(Request);

            if (result.IsSuccess)
            {
                Toast.ShowSuccess("Cliente registrado exitosamente");
                _navigation.NavigateTo(ComponentRoutes.Clients.List);
            }
            else
            {
                Toast.ShowError(result.Message);
            }
        }
    }
}
