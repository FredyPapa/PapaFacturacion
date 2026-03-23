using System;
using System.Collections.Generic;

namespace Papa.Facturacion.Entities;

public partial class Comprobante
{
    public int IComprobante { get; set; }

    public int ITipoComprobanteCat { get; set; }

    public int ITipoPagoCat { get; set; }

    public int ICliente { get; set; }

    public decimal DcTotalBruto { get; set; }

    public decimal? DcIgv { get; set; }

    public decimal DcTotaNeto { get; set; }

    public bool BEstado { get; set; }

    public int IUsuarioCreacion { get; set; }

    public DateTime DFechaCreacion { get; set; }

    public int? IUsuarioModificacion { get; set; }

    public DateTime? DFechaModificacion { get; set; }

    public virtual ICollection<ComprobanteDetalle> ComprobanteDetalles { get; set; } = new List<ComprobanteDetalle>();

    public virtual Cliente IClienteNavigation { get; set; } = null!;

    public virtual CatalogoDetalle ITipoComprobanteCatNavigation { get; set; } = null!;

    public virtual CatalogoDetalle ITipoPagoCatNavigation { get; set; } = null!;
}
