using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Cliente
{
    public class GetClienteResponse
    {
        public int IId { get; set; }

        public int ITipoDocumentoCat { get; set; }

        public string VNumeroDocumento { get; set; } = null!;

        public string VApellidoPaterno { get; set; } = null!;

        public string VApellidoMaterno { get; set; } = null!;

        public string VNombres { get; set; } = null!;

        public string VDireccion { get; set; } = null!;

        public string? VCorreoElectronico { get; set; }

        public string VCelular { get; set; } = null!;
    }
}
