using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class ArticuloMueble : Articulo
    {
        [Display(Name = "Alto"),
        Required(ErrorMessage = "Por favor ingrese un alto.")]
        public double Alto { get; set; }
        [Display(Name = "Ancho"),
        Required(ErrorMessage = "Por favor ingrese un ancho.")]
        public double Ancho { get; set; }
        [Display(Name = "Materia"),
        Required(ErrorMessage = "Por favor ingrese un material.")]
        public string Material { get; set; }
        [Display(Name = "Peso"),
        Required(ErrorMessage = "Por favor ingrese un peso.")]
        public double Peso { get; set; }
        [Display(Name = "Tipo"),
        Required(ErrorMessage = "Por favor ingrese un tipo.")]
        public string Tipo { get; set; }
        [Display(Name = "Fabricante"),
        Required(ErrorMessage = "Por favor ingrese un fabricante.")]
        public string Fabricante { get; set; }
    }
}
