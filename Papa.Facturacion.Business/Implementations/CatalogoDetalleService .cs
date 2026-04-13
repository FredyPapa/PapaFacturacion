using Mapster;
using Microsoft.Extensions.Logging;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.CatalogoDetalle;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class CatalogoDetalleService : ICatalogoDetalleService
    {
        private readonly ICatalogoDetalleRepository _repository;
        private readonly ILogger<CatalogoDetalleService> _logger;

        public CatalogoDetalleService(ICatalogoDetalleRepository repository, ILogger<CatalogoDetalleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear
        public async Task<BaseResponse> AddAsync(CreateCatalogoDetalleRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.AddAsync(request.Adapt<CatalogoDetalle>());
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al registrar el detalle del catálogo";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Actualizar
        public async Task<BaseResponse> UpdateAsync(int id, UpdateCatalogoDetalleRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var catalogoDetalle = await _repository.GetByIdAsync(id);

                if(catalogoDetalle is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CATALOGO_DETALLE_NOT_FOUND";
                    response.Message = "Detalle de Catálogo no encontrado";
                    return response;
                }

                request.Adapt(catalogoDetalle);
                await _repository.UpdateAsync();
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al actualizar el detalle del catálogo";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Obtener por Id
        public async Task<BaseResponse<GetCatalogoDetalleResponse>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<GetCatalogoDetalleResponse>();
            try
            {
                var catalogoDetalle = await _repository.GetByIdAsync(id);

                if (catalogoDetalle is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CATALOGO_DETALLE_NOT_FOUND";
                    response.Message = "Detalle de Catálogo no encontrado";
                    return response;
                }

                response.Result = catalogoDetalle.Adapt<GetCatalogoDetalleResponse>();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al obtener el detalle del catálogo";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Listar sin paginación
        public async Task<BaseResponse<List<ListCatalogoDetalleByCodigoResponse>>> ListAsync(List<string> listCodigos)
        {
            var response = new BaseResponse<List<ListCatalogoDetalleByCodigoResponse>>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado && listCodigos.Any(l => l == p.ICatalogoNavigation.VCodigo),
                        selector: p => new ListCatalogoDetalleByCodigoResponse
                        {
                            Id = p.IId,
                            CodigoPadre = p.ICatalogoNavigation.VCodigo,
                            Codigo = p.VCodigo,
                            Valor = p.VDescripcion!
                        },
                        orderBy: p => p.VDescripcion
                    );

                response.IsSuccess = true;
                response.Result = result.ToList();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al listar los Catalogos.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Listar con paginación
        public async Task<PagedResponse<ListCatalogoDetalleResponse>> ListAsync(SearchListRequest request)
        {
            var response = new PagedResponse<ListCatalogoDetalleResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado &&
                            (
                                (string.IsNullOrEmpty(request.Filter) || p.VCodigo.Contains(request.Filter))
                            ),
                        selector: p => new ListCatalogoDetalleResponse
                        {
                            Id = p.IId,
                            Catalogo = p.ICatalogoNavigation.VDescripcion!,
                            Codigo = p.VCodigo,
                            Descripcion = p.VDescripcion,
                            FechaRegistro = p.DFechaCreacion
                        },
                        orderBy: p => p.VCodigo,
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
                response.Message = "Hubo un error al listar el detalle de catálogos.";
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
                var catalogoDetalle = await _repository.GetByIdAsync(id);

                if (catalogoDetalle is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CATALOGO_DETALLE_NOT_FOUND";
                    response.Message = "Detalle de Catálogo no encontrado.";
                    return response;
                }

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al eliminar el detalle de catálogo.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

    }
}
