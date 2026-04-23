using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Request.CatalogoDetalle
{
    public class CatalogoDetalleRequest
    {
        [Display(Name = "Catálogo")]
        [DeniedValues(0, ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public int ICatalogo { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public string VCodigo { get; set; } = null!;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public string? VDescripcion { get; set; }
    }
}
