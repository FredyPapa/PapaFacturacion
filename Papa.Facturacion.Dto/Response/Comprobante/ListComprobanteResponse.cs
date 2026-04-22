using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Comprobante
{
    public class ListComprobanteResponse
    {
        public int Id { get; set; }

        public int IdTipoComprobante { get; set; }

        [Display(Name = "Tipo de Comprobante")]
        public string TipoComprobante { get; set; } = null!;

        public int IdTipoPago { get; set; }

        [Display(Name = "Tipo de Pago")]
        public string TipoPago { get; set; } = null!;

        public int IdCliente { get; set; }

        [Display]
        public string Cliente { get; set; } = null!;

        [Display(Name = "Total Bruto")]
        public decimal TotalBruto { get; set; }

        [Display]
        public decimal? Igv { get; set; }

        [Display(Name = "Total Neto")]
        public decimal TotalNeto { get; set; }

        [Display(Name = "Cantidad de Productos")]
        public int CantidadProductos { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
