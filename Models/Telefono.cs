using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class Telefono
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoArea { get; set; }
        public string Numero { get; set; }
        public TipoTelefono TipoTelefono { get; set; }
        public string ObtenerTelCompleto()
        {
            return "TipoTelefono: " + TipoTelefono + " Numero: (+" + CodigoPais + ") - " + CodigoArea + " - " +  Numero;
        }
    }
}
