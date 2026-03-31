using System;
using System.Collections.Generic;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Cliente
{
    public class ListClienteResponse
    {
        public int Id { get; set; }

        public string TipoDocumento { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string ApellidoPaterno { get; set; } = null!;

        public string ApellidoMaterno { get; set; } = null!;

        public string Nombres { get; set; } = null!;

        public string Direccion { get; set; } = null!;

        public string? CorreoElectronico { get; set; }

        public string Celular { get; set; } = null!;

        public DateTime FechaRegistro { get; set; }
    }
}
