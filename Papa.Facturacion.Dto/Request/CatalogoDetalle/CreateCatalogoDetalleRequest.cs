using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Request.CatalogoDetalle
{
    public class CreateCatalogoDetalleRequest
    {
        public int ICatalogo { get; set; }

        public string VCodigo { get; set; } = null!;

        public string? VDescripcion { get; set; }
    }
}
