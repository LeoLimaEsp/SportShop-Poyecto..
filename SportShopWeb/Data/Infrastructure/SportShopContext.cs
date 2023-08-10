using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportShopWeb.Domain;

namespace SportShopWeb.Data.Infrastructure
{
    //Heredar de Db context

    public class SportShopContext : IdentityDbContext
    {
        //Crear constructor con configuración de la cadena de conexión
        public SportShopContext(DbContextOptions<SportShopContext> options) : base(options) 
        {

        }

        //Agregar los DBsets para cada entidad del domain, los DBsets ayudan a manipular cada tabla de la base de datos
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<TipoProducto> TipoProductos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
