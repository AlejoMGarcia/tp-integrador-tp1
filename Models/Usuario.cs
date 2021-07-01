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
        MinLength(3, ErrorMessage = "El nombre no puede contener menos de 3 caracteres."),
        MaxLength(30, ErrorMessage = "El nombre no puede contener mas de 30 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor ingrese un nombre."),
        MinLength(3, ErrorMessage = "El apellido no puede contener menos de 3 caracteres."),
        MaxLength(30, ErrorMessage = "El apellido no puede contener mas de 30 caracteres.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El apodo debe ser alfanumerico")]
        [RegularExpression(@"^[a-zA-Z0-9]{6,10}$", ErrorMessage = "El apodo debe ser alfanumerico.")]
        public string Apodo { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public string DNI { get; set; }
        public string CBU { get; set; }
        public string CUIL { get; set; }
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
