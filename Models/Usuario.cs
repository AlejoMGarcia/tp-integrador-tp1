using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
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
