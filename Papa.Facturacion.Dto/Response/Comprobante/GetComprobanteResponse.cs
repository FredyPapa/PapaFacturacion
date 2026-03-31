using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Comprobante
{
    public class GetComprobanteResponse
    {
        public int IId { get; set; }

        public int ITipoComprobanteCat { get; set; }

        public int ITipoPagoCat { get; set; }

        public int ICliente { get; set; }

        public decimal DcTotalBruto { get; set; }

        public decimal? DcIgv { get; set; }

        public decimal DcTotaNeto { get; set; }
    }
}
