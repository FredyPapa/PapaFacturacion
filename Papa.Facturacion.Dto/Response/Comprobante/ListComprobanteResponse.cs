using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Comprobante
{
    public class ListComprobanteResponse
    {
        public int Id { get; set; }

        public string TipoComprobante { get; set; } = null!;

        public string TipoPago { get; set; } = null!;

        public string Cliente { get; set; } = null!;

        public decimal DcTotalBruto { get; set; }

        public decimal? DcIgv { get; set; }

        public decimal DcTotaNeto { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
