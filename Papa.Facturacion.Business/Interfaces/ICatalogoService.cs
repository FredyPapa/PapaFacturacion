using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Catalogo;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Catalogo;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface ICatalogoService
    {
        Task<BaseResponse> AddAsync(CatalogoRequest request);
        Task<BaseResponse> UpdateAsync(int id, CatalogoRequest request);
        Task<BaseResponse<GetCatalogoResponse>> GetByIdAsync(int id);
        Task<BaseResponse<List<ListCatalogoResponse>>> ListAsync();
        Task<PagedResponse<ListCatalogoResponse>> ListAsync(SearchListRequest request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
