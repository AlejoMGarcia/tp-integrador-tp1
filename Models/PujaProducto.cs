using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBasico.Models
{
    public class PujaProducto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ArticuloId { get; set; }
        public int UsuarioIdPuja { get; set; }

        public double PrecioPuja { get; set; }
        public DateTime FechaPuja { get; set; }
    }
}
