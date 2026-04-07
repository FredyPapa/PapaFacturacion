using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Cliente;

namespace Papa.Facturacion.UI.Components.Pages.Maintenance.Clients
{
    public partial class ListClient
    {
        [Inject]
        public IClienteService _service { get; set; } = default!;

        [Inject]
        private IToastService Toast { get; set; } = default!;

        public SearchListRequest Request { get; set; } = new();

        public ICollection<ListClienteResponse> Response { get; set; } = new List<ListClienteResponse>();

        protected override async Task OnInitializedAsync()
        {
            await ListClients();
        }
        private async Task ListClients()
        {
            try
            {
                var result = await _service.ListAsync(Request);
                if (result!.IsSuccess)
                {
                    Response = result.Result;
                }
                else
                {
                    Toast.ShowError(result.Message);
                }
            }
            catch (Exception ex)
            {

                Toast.ShowError($"Hubo un error desconcido: {ex.Message}");
            }
        }
    }
}
