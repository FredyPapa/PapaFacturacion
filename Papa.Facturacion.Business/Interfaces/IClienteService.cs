using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Cliente;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Cliente;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface IClienteService
    {
        Task<BaseResponse> AddAsync(CreateClienteRequest request);
        Task<BaseResponse> UpdateAsync(int id, UpdateClienteRequest request);
        Task<BaseResponse<GetClienteResponse>> GetByIdAsync(int id);
        Task<PagedResponse<ListClienteResponse>> ListAsync(SearchListRequest request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
