using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Entities
{
    public class BaseEntity
    {
        public int IId { get; set; }
        public bool BEstado { get; set; }

        public int IUsuarioCreacion { get; set; }

        public DateTime DFechaCreacion { get; set; }

        public int? IUsuarioModificacion { get; set; }

        public DateTime? DFechaModificacion { get; set; }
    }
}
