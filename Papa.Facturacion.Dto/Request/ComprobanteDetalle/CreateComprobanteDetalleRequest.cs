using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Request.ComprobanteDetalle
{
    public class CreateComprobanteDetalleRequest
    {
        public int IComprobante { get; set; }

        public int IProducto { get; set; }

        public decimal ICantidad { get; set; }

        public decimal DcPrecioUnitario { get; set; }

        public decimal DcTotal { get; set; }
    }
}
