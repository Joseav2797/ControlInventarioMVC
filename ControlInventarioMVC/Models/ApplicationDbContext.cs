using Microsoft.EntityFrameworkCore;

namespace ControlInventarioMVC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
    }
}
