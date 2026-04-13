using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Entities
{
    public class BaseEntity
    {
        public int IId { get; set; }
        public bool BEstado { get; set; } = true;

        public int IUsuarioCreacion { get; set; } = 1;

        public DateTime DFechaCreacion { get; set; } = DateTime.Now;

        public int? IUsuarioModificacion { get; set; }

        public DateTime? DFechaModificacion { get; set; }
    }
}
