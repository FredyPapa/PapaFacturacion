using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.ComprobanteDetalle;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.ComprobanteDetalle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface IComprobanteDetalleService
    {
        Task<BaseResponse> AddAsync(CreateComprobanteDetalleRequest request);
        Task<BaseResponse> UpdateAsync(int id, UpdateComprobanteDetalleRequest request);
        Task<BaseResponse<GetComprobanteDetalleResponse>> GetByIdAsync(int id);
        Task<PagedResponse<ListComprobanteDetalleResponse>> ListAsync(SearchListRequest request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
