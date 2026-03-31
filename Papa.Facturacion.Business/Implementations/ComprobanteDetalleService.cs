using Mapster;
using Microsoft.Extensions.Logging;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.ComprobanteDetalle;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.ComprobanteDetalle;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class ComprobanteDetalleService : IComprobanteDetalleService
    {
        private readonly IComprobanteDetalleRepository _repository;
        private readonly ILogger<ComprobanteDetalleService> _logger;

        public ComprobanteDetalleService(IComprobanteDetalleRepository repository, ILogger<ComprobanteDetalleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear
        public async Task<BaseResponse> AddAsync(CreateComprobanteDetalleRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.AddAsync(request.Adapt<ComprobanteDetalle>());
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al registrar el detalle del comprobante";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Actualizar
        public async Task<BaseResponse> UpdateAsync(int id, UpdateComprobanteDetalleRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var comprobanteDetalle = await _repository.GetByIdAsync(id);

                if(comprobanteDetalle is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "COMPROBANTE_DETALLE_NOT_FOUND";
                    response.Message = "Detalle de Comprobante no encontrado";
                    return response;
                }

                request.Adapt(comprobanteDetalle);
                await _repository.UpdateAsync();
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al actualizar el detalle del comprobante";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Obtener por Id
        public async Task<BaseResponse<GetComprobanteDetalleResponse>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<GetComprobanteDetalleResponse>();
            try
            {
                var comprobanteDetalle = await _repository.GetByIdAsync(id);

                if (comprobanteDetalle is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "COMPROBANTE_DETALLE_NOT_FOUND";
                    response.Message = "Detalle de Comprobante no encontrado";
                    return response;
                }

                response.Result = comprobanteDetalle.Adapt<GetComprobanteDetalleResponse>();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al obtener el detalle del comprobante";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Listar con paginación
        public async Task<PagedResponse<ListComprobanteDetalleResponse>> ListAsync(SearchListRequest request)
        {
            var response = new PagedResponse<ListComprobanteDetalleResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado &&
                            (
                                (string.IsNullOrEmpty(request.Filter) || p.IProductoNavigation.VDescripcion!.Contains(request.Filter))
                            ),
                        selector: p => new ListComprobanteDetalleResponse
                        {
                            Id = p.IId,
                            Comprobante = p.IComprobanteNavigation.IId.ToString()!,
                            Producto = p.IProductoNavigation.VDescripcion!,
                            Cantidad = p.ICantidad,
                            PrecioUnitario = p.DcPrecioUnitario,
                            Total = p.DcTotal,
                            FechaRegistro = p.DFechaCreacion
                        },
                        orderBy: p => p.IId,
                        page: request.Page,
                        pageSize: request.Rows
                    );

                response.IsSuccess = true;
                response.Result = result.Result;
                response.TotalRowPerPages = result.Result.Count;
                response.TotalPages = (int)Math.Ceiling((double)result.TotalRows / request.Rows);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al listar el detalle del comprobantes.";
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
                var comprobanteDetalle = await _repository.GetByIdAsync(id);

                if (comprobanteDetalle is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "COMPROBANTE_DETALLE_NOT_FOUND";
                    response.Message = "Detalle de Comprobante no encontrado.";
                    return response;
                }

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al eliminar el detalle del comprobante.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

    }
}
