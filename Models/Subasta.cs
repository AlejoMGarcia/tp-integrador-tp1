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
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio{ get; set; }
        [Display(Name = "Fecha de Finalización")]
        public DateTime FechaFinalizacion { get; set; }
        public List<Articulo> ArticulosSubastados { get; set; }

    }
}