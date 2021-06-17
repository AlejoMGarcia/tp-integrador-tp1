using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public abstract class Articulo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Precio Inicial")]

        public double PrecioInicial { get; set; }
        [Display(Name = "Precio Mínimo")]
        public double PrecioMinimo { get; set; }
        //public byte[] Foto { get; set; }
        [Display(Name = "Precio en Puja")]
        public double PrecioEnPuja { get; set; }
    }
}
