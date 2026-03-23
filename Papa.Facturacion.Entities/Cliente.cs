using System;
using System.Collections.Generic;

namespace Papa.Facturacion.Entities;

public partial class Cliente
{
    public int ICliente { get; set; }

    public int ITipoDocumentoCat { get; set; }

    public string VNumeroDocumento { get; set; } = null!;

    public string VApellidoPaterno { get; set; } = null!;

    public string VApellidoMaterno { get; set; } = null!;

    public string VNombres { get; set; } = null!;

    public string VDireccion { get; set; } = null!;

    public string? VCorreoElectronico { get; set; }

    public string VCelular { get; set; } = null!;

    public bool BEstado { get; set; }

    public int IUsuarioCreacion { get; set; }

    public DateTime DFechaCreacion { get; set; }

    public int? IUsuarioModificacion { get; set; }

    public DateTime? DFechaModificacion { get; set; }

    public virtual ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();

    public virtual CatalogoDetalle ITipoDocumentoCatNavigation { get; set; } = null!;
}
