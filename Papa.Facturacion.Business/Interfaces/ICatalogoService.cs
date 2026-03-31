using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Catalogo;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Catalogo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface ICatalogoService
    {
        Task<BaseResponse> AddAsync(CreateCatalogoRequest request);
        Task<BaseResponse> UpdateAsync(int id, UpdateCatalogoRequest request);
        Task<BaseResponse<GetCatalogoResponse>> GetByIdAsync(int id);
        Task<PagedResponse<ListCatalogoResponse>> ListAsync(SearchListRequest request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
