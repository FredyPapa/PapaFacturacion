using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.CatalogoDetalle
{
    public class ListCatalogoDetalleResponse
    {
        public int Id { get; set; }

        public string Catalogo { get; set; }

        public string Codigo { get; set; } = null!;

        public string? Descripcion { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
