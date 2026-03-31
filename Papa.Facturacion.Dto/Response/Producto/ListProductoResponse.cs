using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Producto
{
    public class ListProductoResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string Laboratorio { get; set; } = null!;

        public string Categoria { get; set; } = null!;

        public string Marca { get; set; } = null!;

        public decimal PrecioUnitario { get; set; }

        public int Stock { get; set; }

        public DateTime FechaRegistro { get; set; }

    }
}
