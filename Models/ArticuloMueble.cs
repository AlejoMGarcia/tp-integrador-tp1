using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class ArticuloMueble : Articulo
    {
        public double Alto { get; set; }
        public double Ancho { get; set; }
        public string Material { get; set; }
        public double Peso { get; set; }
        public string Tipo { get; set; }
        public string Fabricante { get; set; }
    }
}
