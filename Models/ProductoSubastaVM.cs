using System;

namespace MVCBasico.Models
{
    public class ProductoSubastaVM
    {
        public int SubastaId { get; set; }
        public int ArticuloId { get; set; }
        public bool Included { get; set; }
        public double PrecioInicial { get; set; }
        public double PrecioMinimo { get; set; }

        public string Artista { get; set; }
        public string Periodo { get; set; }
        public TipoArte TipoArte { get; set; }

        public double Alto { get; set; }
        public double Ancho { get; set; }
        public string Material { get; set; }
        public double Peso { get; set; }
        public string Tipo { get; set; }
        public string Fabricante { get; set; }
        public DateTime FechaCreacion { get; set; }
        public TipoArticulo TipoArticulo { get; set; }
        public String Nombre { get; set; }
        public string NombreImagen { get; set; }
        public double PrecioEnPuja { get; set; }

        public double Saldo { get; set; }

    }
}
