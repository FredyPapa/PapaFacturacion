using Mapster;
using Microsoft.Extensions.Logging;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Producto;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Producto;
using Papa.Facturacion.Repositories.Interfaces;
using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        private readonly ILogger<ProductoService> _logger;

        public ProductoService(IProductoRepository repository, ILogger<ProductoService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear
        public async Task<BaseResponse> AddAsync(ProductoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.AddAsync(request.Adapt<Producto>());
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al registrar el producto";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Actualizar
        public async Task<BaseResponse> UpdateAsync(int id, ProductoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var producto = await _repository.GetByIdAsync(id);

                if(producto is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "PRODUCTO_NOT_FOUND";
                    response.Message = "Producto no encontrado";
                    return response;
                }

                request.Adapt(producto);
                await _repository.UpdateAsync();
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al actualizar el producto";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Obtener por Id
        public async Task<BaseResponse<GetProductoResponse>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<GetProductoResponse>();
            try
            {
                var producto = await _repository.GetByIdAsync(id);

                if (producto is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "PRODUCTO_NOT_FOUND";
                    response.Message = "Producto no encontrado";
                    return response;
                }

                response.Result = producto.Adapt<GetProductoResponse>();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al obtener el producto";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Listar con paginación
        public async Task<PagedResponse<ListProductoResponse>> ListAsync(SearchListRequest request)
        {
            var response = new PagedResponse<ListProductoResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado &&
                            (
                                (string.IsNullOrEmpty(request.Filter) || p.VNombre.Contains(request.Filter)) ||
                                (string.IsNullOrEmpty(request.Filter) || p.VDescripcion.Contains(request.Filter))
                            ),
                        selector: p => new ListProductoResponse
                        {
                            Id = p.IId,
                            Nombre = p.VNombre,
                            Descripcion = p.VDescripcion,
                            Laboratorio = p.ILaboratorioCatNavigation.VDescripcion!,
                            Categoria = p.ICategoriaCatNavigation.VDescripcion!,
                            Marca = p.IMarcaCatNavigation.VDescripcion!,
                            PrecioUnitario = p.DcPrecioUnitario,
                            Stock = p.IStock,
                            FechaRegistro = p.DFechaCreacion
                        },
                        orderBy: p => p.VNombre,
                        page: request.Page,
                        pageSize: request.Rows
                    );

                response.IsSuccess = true;
                response.Result = result.Result;
                response.TotalRowPerPages = result.Result.Count;
                response.TotalPages = Helpers.CalculatePageCount(result.TotalRows, request.Rows); //(int)Math.Ceiling((double)result.TotalRows / request.Rows);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al listar productos.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Eliminar
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var producto = await _repository.GetByIdAsync(id);

                if (producto is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "PRODUCTO_NOT_FOUND";
                    response.Message = "Producto no encontrado.";
                    return response;
                }

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al eliminar producto.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

    }
}
