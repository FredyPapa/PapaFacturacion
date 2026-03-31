using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Catalogo
{
    public class ListCatalogoResponse
    {
        public int Id { get; set; }

        public string Codigo { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
