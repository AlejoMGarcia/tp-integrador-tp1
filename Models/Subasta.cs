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

        [Display(Name = "Fecha de Inicio")]
        [Required(ErrorMessage = "La fecha de inicio no puede estar vacia.")]
        public DateTime FechaInicio{ get; set; }

        [Display(Name = "Fecha de Finalización")]
        [Required(ErrorMessage = "La fecha de finalización no puede estar vacia.")]
        public DateTime FechaFinalizacion { get; set; }

        public bool Activa { get; set; }
        public List<Articulo> ArticulosSubastados { get; set; }

        public Subasta()
        {
            this.Activa = true;
            this.FechaInicio = DateTime.Now;
            this.FechaFinalizacion = DateTime.Now;
        }

    }
}