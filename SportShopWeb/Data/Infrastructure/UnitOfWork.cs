using SportShopWeb.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace SportShopWeb.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        // App Context
        SportShopContext context;

        // Atributos Repositories: todas las clases tipo repository
        private ClienteRepository clienteRepository;
        private ProductoRepository productoRepository;
        private VentaRepository ventaRepository;
        private TipoProductoRepository tipoProductoRepository;

        // Dependency injection : por constructor inyectamos el context
        public UnitOfWork(SportShopContext context)
        {
            this.context = context;
        }

        /* 
         * Propiedades: Access properties 
         */
        public ClienteRepository ClienteRepository
        {
            get { return clienteRepository = clienteRepository ?? new ClienteRepository(context); }
        }

        public ProductoRepository ProductoRepository
        {
            get { return productoRepository = productoRepository ?? new ProductoRepository(context); }
        }

        public VentaRepository VentaRepository
        {
            get { return ventaRepository ??= new VentaRepository(context); }
        }

        public TipoProductoRepository TipoProductoRepository
        {
            get { return tipoProductoRepository ??= new TipoProductoRepository(context); }
        }

        /* 
            * Methods: Transaction managment
            */
        public void Commit()
        {
            context.SaveChanges();
        }

        public void Rollback()
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {

                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;

                }
            }
            context.Dispose();
        }

    }
}
