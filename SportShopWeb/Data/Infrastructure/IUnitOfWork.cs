using SportShopWeb.Data.Repository;

namespace SportShopWeb.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        // 1.- Todos los repositories, en forma de propiedad solo get
        ClienteRepository ClienteRepository { get; }
        ProductoRepository ProductoRepository { get; }
        VentaRepository VentaRepository { get; }
        TipoProductoRepository TipoProductoRepository { get; }

        // 2.- Métodos para transacciones
        void Commit();
        void Rollback();
    }
}
