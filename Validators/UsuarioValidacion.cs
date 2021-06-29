using System.ComponentModel.DataAnnotations;
using MVCBasico.Models;
using System.Text.RegularExpressions;

namespace ButterfliesShop.Validators
{
    public class UsuarioValidacion : ValidationAttribute
    {
        private string _pattern;

        public UsuarioValidacion(string pattern)
        {
            _pattern = pattern;
            _largo = largo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Usuario usuario = (Usuario)validationContext.ObjectInstance;
            if (usuario.Apodo != null)
            {

                //"El campo apodo debe ser alfanumerico y contener entre 6 y 10 caracteres.";
            }
            return ValidationResult.Success;
        }

        private ValidationResult validarCampo(string campoAValidar, string mensajePatronInvalido)
        {
            if (campoAValidar.Length > _largo)
            {
                return new ValidationResult(string.Format("El campo contiente {0} caracteres. El limite permitido es de {1}.", campoAValidar.Length, _largo));
            }

            if (!Regex.Match(campoAValidar, _pattern).Success)
            {
                return new ValidationResult(mensajePatronInvalido);
            }
            return ValidationResult.Success;
        }
    }
}
