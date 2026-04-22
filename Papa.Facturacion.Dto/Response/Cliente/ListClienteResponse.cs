using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Response.Cliente
{
    public class ListClienteResponse
    {
        public int Id { get; set; }

        [Display(Name ="Tipo de Documento")]
        public string TipoDocumento { get; set; } = null!;

        [Display(Name = "Número de Documento")]
        public string NumeroDocumento { get; set; } = null!;

        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; } = null!;

        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; } = null!;

        [Display(Name = "Nombres")]
        public string Nombres { get; set; } = null!;

        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = null!;

        [Display(Name = "Correo Electrónico")]
        public string? CorreoElectronico { get; set; }

        [Display(Name = "Celular")]
        public string Celular { get; set; } = null!;

        [Display(Name = "Fecha de Registro")]
        public DateTime FechaRegistro { get; set; }
    }
}
