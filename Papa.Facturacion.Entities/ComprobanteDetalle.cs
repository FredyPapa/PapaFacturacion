using System;
using System.Collections.Generic;

namespace Papa.Facturacion.Entities;

public partial class ComprobanteDetalle
{
    public int IComprobanteDetalle { get; set; }

    public int IComprobante { get; set; }

    public int IProducto { get; set; }

    public decimal ICantidad { get; set; }

    public decimal DcPrecioUnitario { get; set; }

    public decimal DcTotal { get; set; }

    public bool BEstado { get; set; }

    public int IUsuarioCreacion { get; set; }

    public DateTime DFechaCreacion { get; set; }

    public int? IUsuarioModificacion { get; set; }

    public DateTime? DFechaModificacion { get; set; }

    public virtual Comprobante IComprobanteNavigation { get; set; } = null!;

    public virtual Producto IProductoNavigation { get; set; } = null!;
}
