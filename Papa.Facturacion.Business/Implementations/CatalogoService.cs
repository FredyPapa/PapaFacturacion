using Mapster;
using Microsoft.Extensions.Logging;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Catalogo;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Catalogo;
using Papa.Facturacion.Dto.Response.CatalogoDetalle;
using Papa.Facturacion.Dto.Response.Cliente;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class CatalogoService : ICatalogoService
    {
        private readonly ICatalogoRepository _repository;
        private readonly ILogger<CatalogoService> _logger;

        public CatalogoService(ICatalogoRepository repository, ILogger<CatalogoService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear
        public async Task<BaseResponse> AddAsync(CatalogoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                await _repository.AddAsync(request.Adapt<Catalogo>());
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al registrar el catálogo";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Actualizar
        public async Task<BaseResponse> UpdateAsync(int id, CatalogoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var catalogo = await _repository.GetByIdAsync(id);

                if(catalogo is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CATALOGO_NOT_FOUND";
                    response.Message = "Catálogo no encontrado";
                    return response;
                }

                request.Adapt(catalogo);
                await _repository.UpdateAsync();
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al actualizar el catálogo";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Obtener por Id
        public async Task<BaseResponse<GetCatalogoResponse>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<GetCatalogoResponse>();
            try
            {
                var catalogo = await _repository.GetByIdAsync(id);

                if (catalogo is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CATALOGO_NOT_FOUND";
                    response.Message = "Catálogo no encontrado";
                    return response;
                }

                response.Result = catalogo.Adapt<GetCatalogoResponse>();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al obtener el catálogo";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Listar sin paginación
        public async Task<BaseResponse<List<ListCatalogoResponse>>> ListAsync()
        {
            var response = new BaseResponse<List<ListCatalogoResponse>>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado,
                        selector: p => new ListCatalogoResponse
                        {
                            Id = p.IId,
                            Nombre = p.VNombre,
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
        public async Task<PagedResponse<ListCatalogoResponse>> ListAsync(SearchListRequest request)
        {
            var response = new PagedResponse<ListCatalogoResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado &&
                            (
                                (string.IsNullOrEmpty(request.Filter) || p.VCodigo.Contains(request.Filter)) ||
                                (string.IsNullOrEmpty(request.Filter) || p.VNombre.Contains(request.Filter))
                            ),
                        selector: p => new ListCatalogoResponse
                        {
                            Id = p.IId,
                            Codigo = p.VCodigo,
                            Nombre = p.VNombre,
                            Descripcion = p.VDescripcion,
                            FechaRegistro = p.DFechaCreacion
                        },
                        orderBy: p => p.VNombre,
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
                response.Message = "Hubo un error al listar catálogos.";
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
                var catalogo = await _repository.GetByIdAsync(id);

                if (catalogo is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CATALOGO_NOT_FOUND";
                    response.Message = "Catálogo no encontrado.";
                    return response;
                }

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al eliminar catálogo.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

    }
}
