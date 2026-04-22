using Mapster;
using Microsoft.Extensions.Logging;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess;
using Papa.Facturacion.Dto.Request;
using Papa.Facturacion.Dto.Request.Comprobante;
using Papa.Facturacion.Dto.Response;
using Papa.Facturacion.Dto.Response.Comprobante;
using Papa.Facturacion.Repositories.Interfaces;
using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Papa.Facturacion.Business.Implementations
{
    public class ComprobanteService : IComprobanteService
    {
        private readonly IComprobanteRepository _repository;
        private readonly ILogger<ComprobanteService> _logger;
        private readonly IExcelService _excel;

        public ComprobanteService(IComprobanteRepository repository, ILogger<ComprobanteService> logger, IExcelService excel)
        {
            _repository = repository;
            _logger = logger;
            _excel = excel;
        }

        //Crear
        public async Task<BaseResponse> AddAsync(ComprobanteRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var comprobante = request.Adapt<Comprobante>();
                //
                comprobante.IClienteNavigation = null!;
                comprobante.ITipoComprobanteCatNavigation = null!;
                comprobante.ITipoPagoCatNavigation = null!;
                //
                CalculateTotal(comprobante);

                await _repository.CreateAsync(comprobante);
                response.IsSuccess = true;
                response.Message = "Comprobante registrado exitosamente.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Hubo un error al registrar el comprobante";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

        private static void CalculateTotal(Comprobante request)
        {
            _ = request.ComprobanteDetalles.Select(x =>
            {
                x.DcTotal = x.DcPrecioUnitario * x.ICantidad;
                return x;
            }).ToList();

            request.DcTotalBruto = request.ComprobanteDetalles.Sum(x => x.DcTotal);
            request.DcIgv = request.DcTotalBruto * Constants.IGV;
            request.DcTotaNeto = request.DcTotalBruto + (request.DcIgv ?? 0);
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
                            IdTipoComprobante = p.ITipoComprobanteCat,
                            TipoComprobante = p.ITipoComprobanteCatNavigation.VDescripcion!,
                            IdTipoPago = p.ITipoPagoCat,
                            TipoPago = p.ITipoPagoCatNavigation.VDescripcion!,
                            IdCliente = p.ICliente,
                            Cliente = p.IClienteNavigation.VNombres! + " " + p.IClienteNavigation.VApellidoPaterno! + " " + p.IClienteNavigation.VApellidoMaterno!,
                            TotalBruto = p.DcTotalBruto,
                            Igv = p.DcIgv,
                            TotalNeto = p.DcTotaNeto,
                            CantidadProductos = p.ComprobanteDetalles.Count,
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

        //Exportar a Excel
        public async Task<BaseResponse<MemoryStream>> ExportListAsync(SearchListRequest request)
        {
            var response = new BaseResponse<MemoryStream>();
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
                            IdTipoComprobante = p.ITipoComprobanteCat,
                            TipoComprobante = p.ITipoComprobanteCatNavigation.VDescripcion!,
                            IdTipoPago = p.ITipoPagoCat,
                            TipoPago = p.ITipoPagoCatNavigation.VDescripcion!,
                            IdCliente = p.ICliente,
                            Cliente = p.IClienteNavigation.VNombres! + " " + p.IClienteNavigation.VApellidoPaterno! + " " + p.IClienteNavigation.VApellidoMaterno!,
                            TotalBruto = p.DcTotalBruto,
                            Igv = p.DcIgv,
                            TotalNeto = p.DcTotaNeto,
                            CantidadProductos = p.ComprobanteDetalles.Count,
                            FechaRegistro = p.DFechaCreacion
                        },
                        orderBy: p => p.IId,
                        page: request.Page,
                        pageSize: Constants.MaxExportRows
                    );

                response.Result = _excel.ExportExcel(result.Result, "Comprobantes");
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Ocurrió un error al exportar los comprobantes.";
                _logger.LogError(ex, "{0} - {1}", response.Message, ex.Message);
            }
            return response;
        }

    }
}
