using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Request.Catalogo
{
    public class UpdateCatalogoRequest
    {
        public string VCodigo { get; set; } = null!;

        public string VNombre { get; set; } = null!;

        public string? VDescripcion { get; set; }
    }
}
