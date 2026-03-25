using Papa.Facturacion.Entities;
using System;
using System.Collections.Generic;

namespace Papa.Facturacion.DataAccess;

public partial class ComprobanteDetalle : BaseEntity
{
    public int IComprobante { get; set; }

    public int IProducto { get; set; }

    public decimal ICantidad { get; set; }

    public decimal DcPrecioUnitario { get; set; }

    public decimal DcTotal { get; set; }

    public virtual Comprobante IComprobanteNavigation { get; set; } = null!;

    public virtual Producto IProductoNavigation { get; set; } = null!;
}
