using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.CatalogoDetalle;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface ICatalogoDetalleService
    {
        Task<BaseResponse> AddAsync(CatalogoDetalleRequest request);
        Task<BaseResponse> UpdateAsync(int id, CatalogoDetalleRequest request);
        Task<BaseResponse<GetCatalogoDetalleResponse>> GetByIdAsync(int id);
        Task<BaseResponse<List<ListCatalogoDetalleByCodigoResponse>>> ListAsync(List<string> listCodigos);
        Task<PagedResponse<ListCatalogoDetalleResponse>> ListAsync(SearchListRequest request);
        Task<BaseResponse> DeleteAsync(int id);
        Task<BaseResponse<MemoryStream>> ExportListAsync(SearchListRequest request);
    }
}
