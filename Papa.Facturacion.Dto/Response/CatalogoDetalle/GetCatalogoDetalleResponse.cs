using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.CatalogoDetalle
{
    public class GetCatalogoDetalleResponse
    {
        public int IId { get; set; }

        public int ICatalogo { get; set; }

        public string VCodigo { get; set; } = null!;

        public string? VDescripcion { get; set; }
    }
}
