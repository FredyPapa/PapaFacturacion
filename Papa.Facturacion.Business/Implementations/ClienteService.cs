using Mapster;
using Microsoft.Extensions.Logging;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Cliente;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Cliente;
using Papa.Facturacion.Repositories.Interfaces;
using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository repository, ILogger<ClienteService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //Crear
        public async Task<BaseResponse> AddAsync(ClienteRequest request)
        {
            var response = new BaseResponse();
            try
            {
                /*
                var cliente = new Cliente()
                {
                    ITipoDocumentoCat = request.ITipoDocumentoCat,
                    VNumeroDocumento = request.VNumeroDocumento,
                    VApellidoPaterno = request.VApellidoPaterno,
                    VApellidoMaterno = request.VApellidoMaterno,
                    VNombres = request.VNombres,
                    VDireccion = request.VDireccion,
                    VCorreoElectronico = request.VCorreoElectronico,
                    VCelular = request.VCelular,
                };
                await _repository.AddAsync(cliente);
                */
                await _repository.AddAsync(request.Adapt<Cliente>());
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al registrar el cliente";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Actualizar
        public async Task<BaseResponse> UpdateAsync(int id,ClienteRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var cliente = await _repository.GetByIdAsync(id);

                if(cliente is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CLIENTE_NOT_FOUND";
                    response.Message = "Cliente no encontrado";
                    return response;
                }

                request.Adapt(cliente);
                await _repository.UpdateAsync();
                response.IsSuccess = true;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al actualizar el cliente";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Obtener por Id
        public async Task<BaseResponse<GetClienteResponse>> GetByIdAsync(int id)
        {
            var response = new BaseResponse<GetClienteResponse>();
            try
            {
                var cliente = await _repository.GetByIdAsync(id);

                if (cliente is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CLIENTE_NOT_FOUND";
                    response.Message = "Cliente no encontrado";
                    return response;
                }

                response.Result = cliente.Adapt<GetClienteResponse>();
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al obtener el cliente";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Listar con paginación
        public async Task<PagedResponse<ListClienteResponse>> ListAsync(SearchListRequest request)
        {
            var response = new PagedResponse<ListClienteResponse>();
            try
            {
                var result = await _repository.ListAsync(
                        predicate: p => p.BEstado &&
                            (
                                (string.IsNullOrEmpty(request.Filter) || p.VApellidoPaterno.Contains(request.Filter)) ||
                                (string.IsNullOrEmpty(request.Filter) || p.VApellidoPaterno.Contains(request.Filter)) ||
                                (string.IsNullOrEmpty(request.Filter) || p.VNombres.Contains(request.Filter))
                            ),
                        selector: p => new ListClienteResponse
                        {
                            Id = p.IId,
                            TipoDocumento = p.ITipoDocumentoCatNavigation.VDescripcion!,
                            NumeroDocumento = p.VNumeroDocumento,
                            ApellidoPaterno = p.VApellidoPaterno,
                            ApellidoMaterno = p.VApellidoMaterno,
                            Nombres = p.VNombres,
                            Direccion = p.VDireccion,
                            CorreoElectronico = p.VCorreoElectronico,
                            Celular = p.VCelular,
                            FechaRegistro = p.DFechaCreacion
                        },
                        orderBy: p => p.VApellidoPaterno,
                        page: request.Page,
                        pageSize: request.Rows
                    );

                response.IsSuccess = true;
                response.Result = result.Result;
                response.TotalRowPerPages = result.Result.Count;
                response.TotalPages = response.TotalPages = Helpers.CalculatePageCount(result.TotalRows, request.Rows); //(int)Math.Ceiling((double)result.TotalRows / request.Rows);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al listar clientes.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        //Eliminar Cliente
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();
            try
            {
                var cliente = await _repository.GetByIdAsync(id);

                if (cliente is null)
                {
                    response.IsSuccess = true;
                    response.ErrorCode = "CLIENTE_NOT_FOUND";
                    response.Message = "Cliente no encontrado.";
                    return response;
                }

                await _repository.DeleteAsync(id);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al eliminar cliente.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

    }
}
