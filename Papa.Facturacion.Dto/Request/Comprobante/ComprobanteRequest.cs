using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text;

namespace Papa.Facturacion.Dto.Request.Comprobante
{
    public class ComprobanteRequest
    {
        public int TipoComprobante { get; set; }

        public int TipoPago { get; set; }

        public int IdCliente { get; set; }

        public string NombreCliente { get; set; } = default!;
        
        public List<ComprobanteDetalleRequest> ComprobanteDetalles { get; set; } = new();

        /*public decimal TotalBruto => ComprobanteDetalles.Sum(d => d.Total);

        public decimal IGV => TotalBruto * Constants.IGV;

        public decimal TotalNeto => TotalBruto + IGV;*/

    }

    public class ComprobanteDetalleRequest
    {

        public int IdProducto { get; set; }

        public string NombreProducto { get; set; } = default!;

        public string Marca { get; set; } = default!;

        [Range(1,int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo")]
        public decimal Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Total => PrecioUnitario * Cantidad;

    }
}
