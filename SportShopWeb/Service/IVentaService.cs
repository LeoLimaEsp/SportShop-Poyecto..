using SportShopWeb.Models;

namespace SportShopWeb.Service
{
    public interface IVentaService
    {
        // Definir las operaciones de Servicio
        void Create(VentaModel venta);
        //void Update(VentaModel venta);
        //void Delete(int IdVenta);
        IList<VentaModel> GetAll();
        VentaModel Get(int id);
        VentaModel Get();
    }
}
