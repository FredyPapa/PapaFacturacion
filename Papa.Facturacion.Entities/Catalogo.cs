using System;
using System.Collections.Generic;

namespace Papa.Facturacion.Entities;

public partial class Catalogo
{
    public int ICatalogo { get; set; }

    public string VCodigo { get; set; } = null!;

    public string VNombre { get; set; } = null!;

    public string? VDescripcion { get; set; }

    public bool BEstado { get; set; }

    public int IUsuarioCreacion { get; set; }

    public DateTime DFechaCreacion { get; set; }

    public int? IUsuarioModificacion { get; set; }

    public DateTime? DFechaModificacion { get; set; }

    public virtual ICollection<CatalogoDetalle> CatalogoDetalles { get; set; } = new List<CatalogoDetalle>();
}
