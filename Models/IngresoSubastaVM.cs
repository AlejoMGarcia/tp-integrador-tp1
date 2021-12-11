using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCBasico.Models
{
    public class IngresoSubastaVM
    {
        public int SubastaId { get; set; }
        public string Codigo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public int CantidadProductosArte { get; set; }
        public int CantidadProductosMueble { get; set; }
        public int CantidadProductos { get; set; }

        public int UsuarioId { get; set; }

    }
}
