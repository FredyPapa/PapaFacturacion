using System;
using System.Collections.Generic;

namespace Papa.Facturacion.Entities;

public partial class Producto
{
    public int IProducto { get; set; }

    public string VNombre { get; set; } = null!;

    public string VDescripcion { get; set; } = null!;

    public int ILaboratorioCat { get; set; }

    public int ICategoriaCat { get; set; }

    public int IMarcaCat { get; set; }

    public decimal DcPrecioUnitario { get; set; }

    public int IStock { get; set; }

    public bool BEstado { get; set; }

    public int IUsuarioCreacion { get; set; }

    public DateTime DFechaCreacion { get; set; }

    public int? IUsuarioModificacion { get; set; }

    public DateTime? DFechaModificacion { get; set; }

    public virtual ICollection<ComprobanteDetalle> ComprobanteDetalles { get; set; } = new List<ComprobanteDetalle>();

    public virtual CatalogoDetalle ICategoriaCatNavigation { get; set; } = null!;

    public virtual CatalogoDetalle ILaboratorioCatNavigation { get; set; } = null!;

    public virtual CatalogoDetalle IMarcaCatNavigation { get; set; } = null!;
}
