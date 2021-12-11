using System;

namespace MVCBasico.Models
{
    public class ProductoSubastaVM
    {
        public int SubastaId { get; set; }
        public int ArticuloId { get; set; }
        public bool Included { get; set; }
        public double PrecioInicial { get; set; }

        public DateTime FechaCreacion { get; set; }
        public TipoArticulo TipoArticulo { get; set; }
        public String Nombre { get; set; }
        public string NombreImagen { get; set; }
        public double PrecioEnPuja { get; set; }
    }
}
