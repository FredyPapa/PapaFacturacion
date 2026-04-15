using Papa.Facturacion.Dto.Attributes;
using Papa.Facturacion.Dto.Interfaces;
using Papa.Facturacion.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Request.Cliente
{
    public class ClienteRequest : IIdentificableDocumento
    {
        [Display(Name = "Tipo de Documento")]
        [DeniedValues(0, ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        public int ITipoDocumentoCat { get; set; }

        private string _vNumeroDocumento = null!; // Campo privado para guardar el valor real
        [Display(Name = "Número de Documento")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "El {0} debe contener solo números.")]
        [DniOthersValidation]
        public string VNumeroDocumento { 
            get => _vNumeroDocumento?.Trim() ?? string.Empty; 
            set => _vNumeroDocumento = value; 
        }

        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [StringLength(80, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VApellidoPaterno { get; set; } = null!;

        [Display(Name = "Apellido Materno")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [StringLength(80, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VApellidoMaterno { get; set; } = null!;

        [Display(Name = "Nombres")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [StringLength(100, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VNombres { get; set; } = null!;

        [Display(Name = "Dirección")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [StringLength(200, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        public string VDireccion { get; set; } = null!;

        [Display(Name = "Correo Electrónico")]
        [StringLength(200, ErrorMessage = "El {0} no puede exceder los {1} caracteres.")]
        [EmailAddress(ErrorMessage = "El formato del {0} no es válido.")]
        public string? VCorreoElectronico { get; set; }

        private string _vCelular = null!; // Campo privado para guardar el valor real
        [Display(Name = "Celular")]
        [Required(ErrorMessage = DataAnnotationMessage.RequiredMessage)]
        [RegularExpression(@"^9[0-9]{8}$", ErrorMessage = "El {0} debe empezar con 9 y tener 9 dígitos.")]
        public string VCelular {
            get => _vCelular?.Trim() ?? string.Empty; 
            set => _vCelular = value; 
        }

    }
}
