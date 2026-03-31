using Mapster;
using Microsoft.Extensions.Logging;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Comprobante;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Comprobante;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class ComprobanteService : IComprobanteService
    {
        private readonly IComprobanteRepository _repository;
        private readonly ILogger<ComprobanteService> _logger;

        public ComprobanteService(IComprobanteRepository repository, ILogger<ComprobanteService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear
        public async Task<BaseResponse> AddAsync(CreateComprobanteRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.AddAsync(request.Adapt<Comprobante>());
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al registrar el comprobante";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Actualizar
        public async Task<BaseResponse> UpdateAsync(int id, UpdateComprobanteRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var comprobante = await _repository.GetByIdAsync(id);

                if(comprobante is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "COMPROBANTE_NOT_FOUND";
                    response.Message = "Comprobante no encontrado";
                    return response;
                }

                request.Adapt(comprobante);
                await _repository.UpdateAsync();
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al actualizar el comprobante";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Obtener por Id
        public async Task<BaseResponse<GetComprobanteResponse>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<GetComprobanteResponse>();
            try
            {
                var comprobante = await _repository.GetByIdAsync(id);

                if (comprobante is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "COMPROBANTE_NOT_FOUND";
                    response.Message = "Comprobante no encontrado";
                    return response;
                }

                response.Result = comprobante.Adapt<GetComprobanteResponse>();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al obtener el comprobante";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Listar con paginación
        public async Task<PagedResponse<ListComprobanteResponse>> ListAsync(SearchListRequest request)
        {
            var response = new PagedResponse<ListComprobanteResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado &&
                            (
                                (string.IsNullOrEmpty(request.Filter) || p.ITipoComprobanteCatNavigation.VDescripcion!.Contains(request.Filter)) ||
                                (string.IsNullOrEmpty(request.Filter) || p.ITipoPagoCatNavigation.VDescripcion!.Contains(request.Filter)) ||
                                (string.IsNullOrEmpty(request.Filter) || (p.IClienteNavigation.VNombres! + " " + p.IClienteNavigation.VApellidoPaterno! + " " + p.IClienteNavigation.VApellidoMaterno!).Contains(request.Filter))
                            ),
                        selector: p => new ListComprobanteResponse
                        {
                            Id = p.IId,
                            TipoComprobante = p.ITipoComprobanteCatNavigation.VDescripcion!,
                            TipoPago = p.ITipoPagoCatNavigation.VDescripcion!,
                            Cliente = p.IClienteNavigation.VNombres! + " " + p.IClienteNavigation.VApellidoPaterno! + " " + p.IClienteNavigation.VApellidoMaterno!,
                            DcTotalBruto = p.DcTotalBruto,
                            DcIgv = p.DcIgv,
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
                response.Message = "Hubo un error al listar comprobantes.";
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
                var comprobante = await _repository.GetByIdAsync(id);

                if (comprobante is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "COMPROBANTE_NOT_FOUND";
                    response.Message = "Comprobante no encontrado.";
                    return response;
                }

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al eliminar comprobante.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

    }
}
