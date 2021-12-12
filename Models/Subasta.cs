using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class Subasta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El código de la subasta no puede estar vacío.")]
        //[RegularExpression(@"^[a-zA-Z''-'\s]", ErrorMessage = "El código solo acepta caracteres alfabeticos.")]
        public string Codigo { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Inicio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [Required(ErrorMessage = "La fecha de inicio no puede estar vacia.")]
        public DateTime FechaInicio{ get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Fecha de Finalización")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [Required(ErrorMessage = "La fecha de finalización no puede estar vacia.")]
        public DateTime FechaFinalizacion { get; set; }

        public bool Activa { get; set; }

        [ForeignKey("Usuario")]
        public int? UsuarioId { get; set; }
        public List<Articulo> ArticulosSubastados { get; set; }

        public Subasta()
        {
            this.Activa = true;
            this.FechaInicio = DateTime.Today;
            this.FechaFinalizacion = DateTime.Today;
        }

    }
}