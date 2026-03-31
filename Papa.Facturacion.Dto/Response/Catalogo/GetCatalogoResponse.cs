using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Catalogo
{
    public class GetCatalogoResponse
    {
        public int IId { get; set; }

        public string VCodigo { get; set; } = null!;

        public string VNombre { get; set; } = null!;

        public string? VDescripcion { get; set; }
    }
}
