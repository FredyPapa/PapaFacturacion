using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Producto
{
    public class ListProductoResponse
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = null!;

        [Display(Name = "Laboratorio")]
        public string Laboratorio { get; set; } = null!;

        [Display(Name = "Categoría")]
        public string Categoria { get; set; } = null!;

        [Display(Name = "Marca")]
        public string Marca { get; set; } = null!;

        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [Display(Name = "Stock")]
        public int Stock { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }

    }
}
