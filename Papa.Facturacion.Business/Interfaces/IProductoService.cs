using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Producto;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Producto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Interfaces
{
    public interface IProductoService
    {
        Task<BaseResponse> AddAsync(CreateProductoRequest request);
        Task<BaseResponse> UpdateAsync(int id, UpdateProductoRequest request);
        Task<BaseResponse<GetProductoResponse>> GetByIdAsync(int id);
        Task<PagedResponse<ListProductoResponse>> ListAsync(SearchListRequest request);
        Task<BaseResponse> DeleteAsync(int id);
    }
}
