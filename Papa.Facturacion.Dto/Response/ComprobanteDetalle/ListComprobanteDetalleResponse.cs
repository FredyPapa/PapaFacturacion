using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.ComprobanteDetalle
{
    public class ListComprobanteDetalleResponse
    {
        public int Id { get; set; }

        public string Comprobante { get; set; } = null!;

        public string Producto { get; set; } = null!;

        public decimal Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        public decimal Total { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
