using SportShopWeb.Models;

namespace SportShopWeb.Service
{
    public interface ICatalogService
    {
        // Definir las operaciones de Servicio
        void Create(TipoProductoModel tipoProducto);
        void Update(TipoProductoModel tipoProducto);
        void Delete(int IdTipoProducto);
        IList<TipoProductoModel> GetAll();
        TipoProductoModel Get(int id);
    }
}
