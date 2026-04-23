using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Request.Catalogo
{
    public class CatalogoRequest
    {
        public string VCodigo { get; set; } = null!;

        public string VNombre { get; set; } = null!;

        public string? VDescripcion { get; set; }
    }
}
