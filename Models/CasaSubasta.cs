using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class CasaSubasta
    {
        public string Nombre { get; set; }
        public List<Usuario> Usuarios { get; set; }
    }
}
