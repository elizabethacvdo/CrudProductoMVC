using CRUD_PRODUCTOSMVC.Models;
using Microsoft.EntityFrameworkCore;


namespace CRUD_PRODUCTOSMVC
{
    public class productoContext : DbContext
    {


        public productoContext(DbContextOptions<productoContext> options)
            : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
      



    }
}
