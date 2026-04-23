using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Response.CatalogoDetalle
{
    public class ListCatalogoDetalleResponse
    {
        public int Id { get; set; }

        [Display(Name = "Catálogo")]
        public string Catalogo { get; set; }

        [Display(Name = "Código")]
        public string Codigo { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
