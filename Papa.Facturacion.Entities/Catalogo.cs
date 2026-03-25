using Papa.Facturacion.Entities;
using System;
using System.Collections.Generic;

namespace Papa.Facturacion.DataAccess;

public partial class Catalogo : BaseEntity
{

    public string VCodigo { get; set; } = null!;

    public string VNombre { get; set; } = null!;

    public string? VDescripcion { get; set; }


    public virtual ICollection<CatalogoDetalle> CatalogoDetalles { get; set; } = new List<CatalogoDetalle>();
}
