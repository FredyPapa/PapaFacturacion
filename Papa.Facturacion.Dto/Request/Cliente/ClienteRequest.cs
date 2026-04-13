using Papa.Facturacion.Dto.Attributes;
using Papa.Facturacion.Dto.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Request.Cliente
{
    public class ClienteRequest : IIdentificableDocumento
    {
        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ITipoDocumentoCat { get; set; }

        private string _vNumeroDocumento = null!; // Campo privado para guardar el valor real
        [Display(Name = "Número de Documento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El {0} debe contener solo números.")]
        [DniOthersValidation]
        public string VNumeroDocumento { 
            get => _vNumeroDocumento?.Trim() ?? string.Empty; 
            set => _vNumeroDocumento = value; 
        }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(80, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VApellidoPaterno { get; set; } = null!;

        [Display(Name = "Apellido Materno")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(80, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VApellidoMaterno { get; set; } = null!;

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(100, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VNombres { get; set; } = null!;

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [StringLength(200, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VDireccion { get; set; } = null!;

        [Display(Name = "Correo Electrónico")]
        [StringLength(200, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del {0} no es válido.")]
        public string? VCorreoElectronico { get; set; }

        private string _vCelular = null!; // Campo privado para guardar el valor real
        [Display(Name = "Celular")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [RegularExpression(@"^9[0-9]{8}$", ErrorMessage = "El {0} debe empezar con 9 y tener 9 dígitos.")]
        public string VCelular {
            get => _vCelular?.Trim() ?? string.Empty; 
            set => _vCelular = value; 
        }

    }
}
