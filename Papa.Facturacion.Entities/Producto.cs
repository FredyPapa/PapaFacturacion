using Papa.Facturacion.Entities;
using System;
using System.Collections.Generic;

namespace Papa.Facturacion.DataAccess;

public partial class Producto : BaseEntity
{
    public string VNombre { get; set; } = null!;

    public string VDescripcion { get; set; } = null!;

    public int ILaboratorioCat { get; set; }

    public int ICategoriaCat { get; set; }

    public int IMarcaCat { get; set; }

    public decimal DcPrecioUnitario { get; set; }

    public int IStock { get; set; }

    public virtual ICollection<ComprobanteDetalle> ComprobanteDetalles { get; set; } = new List<ComprobanteDetalle>();

    public virtual CatalogoDetalle ICategoriaCatNavigation { get; set; } = null!;

    public virtual CatalogoDetalle ILaboratorioCatNavigation { get; set; } = null!;

    public virtual CatalogoDetalle IMarcaCatNavigation { get; set; } = null!;
}
