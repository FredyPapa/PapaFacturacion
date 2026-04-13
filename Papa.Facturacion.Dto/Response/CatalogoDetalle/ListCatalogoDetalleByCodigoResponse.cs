using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.CatalogoDetalle
{
    public class ListCatalogoDetalleByCodigoResponse
    {
        public int Id { get; set; }
        public string CodigoPadre { get; set; } = default!;
        public string Codigo { get; set; } = default!;
        public string Valor { get; set; } = default!;
    }
}
