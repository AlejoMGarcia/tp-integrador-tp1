using Microsoft.EntityFrameworkCore;
using MVCBasico.Models;
namespace MVCBasico.Context
{
    public class SubastaDatabaseContext : DbContext
    {
        public
       SubastaDatabaseContext(DbContextOptions<SubastaDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Telefono> Telefonos { get; set;}
        public DbSet<ArticuloMueble> ArticulosMueble { get; set; }
        public DbSet<ArticuloArte> ArticulosArte { get; set; }

        public DbSet<Subasta> Subastas { get; set; }
    }
}