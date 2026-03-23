using System;
using System.Collections.Generic;

namespace Papa.Facturacion.Entities;

public partial class CatalogoDetalle
{
    public int ICatalogoDetalle { get; set; }

    public int ICatalogo { get; set; }

    public string VCodigo { get; set; } = null!;

    public string? VDescripcion { get; set; }

    public bool BEstado { get; set; }

    public int IUsuarioCreacion { get; set; }

    public DateTime DFechaCreacion { get; set; }

    public int? IUsuarioModificacion { get; set; }

    public DateTime? DFechaModificacion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Comprobante> ComprobanteITipoComprobanteCatNavigations { get; set; } = new List<Comprobante>();

    public virtual ICollection<Comprobante> ComprobanteITipoPagoCatNavigations { get; set; } = new List<Comprobante>();

    public virtual Catalogo ICatalogoNavigation { get; set; } = null!;

    public virtual ICollection<Producto> ProductoICategoriaCatNavigations { get; set; } = new List<Producto>();

    public virtual ICollection<Producto> ProductoILaboratorioCatNavigations { get; set; } = new List<Producto>();

    public virtual ICollection<Producto> ProductoIMarcaCatNavigations { get; set; } = new List<Producto>();
}
