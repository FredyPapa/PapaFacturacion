using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Request.Producto
{
    public class ProductoRequest
    {
        [Display(Name = "Nombre del producto")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public string VNombre { get; set; } = null!;

        [Display(Name = "Descripción del producto")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public string VDescripcion { get; set; } = null!;

        [Display(Name = "Laboratorio")]
        [DeniedValues(0, ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public int ILaboratorioCat { get; set; }

        [Display(Name = "Categoría")]
        [DeniedValues(0, ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public int ICategoriaCat { get; set; }

        [Display(Name = "Marca")]
        [DeniedValues(0, ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public int IMarcaCat { get; set; }

        [Display(Name = "Precio unitario")]
        [DeniedValues(0, ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [Range(0.00, 9999.99, ErrorMessage = "El {0} debe estar entre {1} y {2}.")]
        [RegularExpression(@"^\d{1,4}(\.\d{1,2})?$", ErrorMessage = "El {0} solo permite hasta 4 enteros y 2 decimales.")]
        public decimal DcPrecioUnitario { get; set; }


        [Display(Name = "Stock")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [Range(0, 9999, ErrorMessage = "El {0} debe ser un número entre {1} y {2}.")]
        public int IStock { get; set; }
    }
}
