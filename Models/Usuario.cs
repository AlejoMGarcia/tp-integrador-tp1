using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{

    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre."),
        RegularExpression(@"^[a-zA-Z]{3,30}$", ErrorMessage = "El nombre debe ser alfabético t tener entre 3 y 30 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un apellido."),
        RegularExpression(@"^[a-zA-Z]{3,30}$", ErrorMessage = "El apellido debe ser alfabético t tener entre 3 y 30 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un apodo"),
        RegularExpression(@"^[a-zA-Z0-9]{6,10}$", ErrorMessage = "El apodo debe ser alfanumerico y tener entre 6 y 10 caracteres.")]
        public string Apodo { get; set; }

        [Required(ErrorMessage = "Por favor ingrese una contraseña"),
        RegularExpression(@"^[a-zA-Z0-9]{8,12}$", ErrorMessage = "La contraseña debe ser alfanumerica y tener entre 8 y 12 caracteres.")]
        public string Contraseña { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un email"),
        RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "El email no cumple el formato")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un DNI"),
        RegularExpression(@"^[0-9]{1,8}$", ErrorMessage = "El DNI debe ser numérico y tener entre 1 y 8 caracteres.")]
        public string DNI { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un CBU"),
        RegularExpression(@"^[0-9]{22}$", ErrorMessage = "El CBU debe ser numérico y tener 22 caracteres")]
        public string CBU { get; set; }

        //Este cuil acepta dos formatos: con y sin '-'
        [Required(ErrorMessage = "Por favor ingrese un CUIL"),
        RegularExpression(@"^([0-9]{11}|[0-9]{2}-[0-9]{8}-[0-9]{1})$", ErrorMessage = "El no cumple el formato.")]
        public string CUIL { get; set; }

        [RegularExpression(@"^(?!(?:0|0\.0|0\.00)$)[+]?\d+(\.\d|\.\d[0-9])?$", ErrorMessage = "El saldo no puede ser negativo ni tener mas de dos digitos después de la coma.")]
        public double Saldo { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNaciemiento { get; set; }
        public TipoGenero Genero { get; set; }
        public EstadoUsuario Estado { get; set; }
        public List<Telefono> Telefonos { get; set; }

        public List<Compra> Compras { get; set; }
        public List<Subasta> Subastas { get; set; }
    }
}
