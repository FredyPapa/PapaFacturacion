using Papa.Facturacion.Dto.Interfaces;
using Papa.Facturacion.Dto.Request.Cliente;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Papa.Facturacion.Dto.Attributes
{
    public class DniOthersValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Obtenemos la instancia del objeto que se está validando
            var request = validationContext.ObjectInstance as IIdentificableDocumento;
            if (request == null) return ValidationResult.Success; // O maneja el error
            var numeroDoc = value as string;

            if (string.IsNullOrEmpty(numeroDoc)) return ValidationResult.Success;

            // Lógica condicional: 10 = DNI (8 dígitos), otro = 9 dígitos
            int longitudRequerida = (request.ITipoDocumentoCat == 10) ? 8 : 9;

            if (numeroDoc.Length != longitudRequerida)
            {
                return new ValidationResult($"Para el tipo de documento seleccionado, el número debe tener {longitudRequerida} dígitos.",
                new[] { validationContext.MemberName! });
            }

            return ValidationResult.Success;
        }
    }
}
