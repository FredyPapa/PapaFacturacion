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
        public int ITipoComprobanteCat { get; set; }

        public int ITipoPagoCat { get; set; }

        public int ICliente { get; set; }

        public string NombreCliente { get; set; } = default!;
        
        public List<ComprobanteDetalleRequest> ComprobanteDetalles { get; set; } = new();

        public decimal DcTotalBruto { get; set; }

        public decimal DcIgv { get; set; }

        public decimal DcTotaNeto { get; set; }

    }

    public class ComprobanteDetalleRequest
    {

        public int IProducto { get; set; }

        public string NombreProducto { get; set; } = default!;

        public string Marca { get; set; } = default!;

        [Range(1,int.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo")]
        public decimal ICantidad { get; set; }

        public decimal DcPrecioUnitario { get; set; }

        public decimal DcTotal => DcPrecioUnitario * ICantidad;

    }
}
