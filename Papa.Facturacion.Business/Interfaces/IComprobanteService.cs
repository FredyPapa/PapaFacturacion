using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Comprobante;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Comprobante;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface IComprobanteService
    {
        Task<BaseResponse> AddAsync(ComprobanteRequest request);
        Task<PagedResponse<ListComprobanteResponse>> ListAsync(SearchListRequest request);
    }
}
