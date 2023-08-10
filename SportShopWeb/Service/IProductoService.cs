using SportShopWeb.Models;

namespace SportShopWeb.Service
{
    public interface IProductoService
    {
        // Definir las operaciones de Servicio
        void Create(ProductoModel producto);
        void Update(ProductoModel producto);
        void Delete(int idProducto);
        IList<ProductoModel> GetAll();
        ProductoModel Get(int id);
        ProductoModel Get();
    }
}
