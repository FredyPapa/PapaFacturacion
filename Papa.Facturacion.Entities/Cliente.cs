using Papa.Facturacion.Entities;
using System;
using System.Collections.Generic;

namespace Papa.Facturacion.DataAccess;

public partial class Cliente : BaseEntity
{
    public int ITipoDocumentoCat { get; set; }

    public string VNumeroDocumento { get; set; } = null!;

    public string VApellidoPaterno { get; set; } = null!;

    public string VApellidoMaterno { get; set; } = null!;

    public string VNombres { get; set; } = null!;

    public string VDireccion { get; set; } = null!;

    public string? VCorreoElectronico { get; set; }

    public string VCelular { get; set; } = null!;

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();

    public virtual CatalogoDetalle ITipoDocumentoCatNavigation { get; set; } = null!;
}
