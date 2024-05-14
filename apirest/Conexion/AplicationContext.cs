using apirest.Model;
using Microsoft.EntityFrameworkCore;

namespace apirest.Conexion
{
    public class AplicationContext : DbContext
    {
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> categoria { get; set; }
        public DbSet<Libro> libro { get; set; }
        public DbSet<User> User { get; set; }
        public AplicationContext(DbContextOptions<AplicationContext> options) : base(options)
        {
        }




    }
}
