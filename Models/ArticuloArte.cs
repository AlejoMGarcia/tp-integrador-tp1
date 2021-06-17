using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class ArticuloArte : Articulo
    {
        public string Artista { get; set; }
        public string Periodo { get; set; }
        [Display(Name = "Tipo de Arte")]
        public TipoArte TipoArte { get; set; }
    }
}
