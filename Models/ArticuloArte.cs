using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class ArticuloArte : Articulo
    {
        [Display(Name = "Artista"),
        Required(ErrorMessage = "Por favor ingrese un artista.")]
        public string Artista { get; set; }
        [Display(Name = "Período"),
        Required(ErrorMessage = "Por favor ingrese un período.")]
        public string Periodo { get; set; }
        [Display(Name = "Tipo de Arte"),
        Required(ErrorMessage = "Por favor ingrese un tipo de arte.")]
        public TipoArte TipoArte { get; set; }
    }
}
