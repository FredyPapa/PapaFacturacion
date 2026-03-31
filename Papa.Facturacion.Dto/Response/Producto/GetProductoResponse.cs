using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Producto
{
    public class GetProductoResponse
    {
        public int IId { get; set; }

        public string VNombre { get; set; } = null!;

        public string VDescripcion { get; set; } = null!;

        public int ILaboratorioCat { get; set; }

        public int ICategoriaCat { get; set; }

        public int IMarcaCat { get; set; }

        public decimal DcPrecioUnitario { get; set; }

        public int IStock { get; set; }
    }
}
