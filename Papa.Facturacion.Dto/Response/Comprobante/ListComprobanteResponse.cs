using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Comprobante
{
    public class ListComprobanteResponse
    {
        public int Id { get; set; }

        public int IdTipoComprobante { get; set; }

        public string TipoComprobante { get; set; } = null!;

        public int IdTipoPago { get; set; }

        public string TipoPago { get; set; } = null!;

        public int IdCliente { get; set; }

        public string Cliente { get; set; } = null!;

        public decimal TotalBruto { get; set; }

        public decimal? Igv { get; set; }

        public decimal TotalNeto { get; set; }

        public int CantidadProductos { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
