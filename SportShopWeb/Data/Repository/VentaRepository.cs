using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;

namespace SportShopWeb.Data.Repository
{
    public class VentaRepository
    {
        /*
        Las clases tipo repository contienen las operaciones a la tabla correspondiente (VENTA).
        Tipicamente el CRUD + unas consultas personalizadas. 
        Todas las operaciones se realizan mediante el context. 
        */

        SportShopContext context; //Objeto vacio, se requiere inyeccion

        //Inyección del objeto: creando un constructor
        public VentaRepository(SportShopContext context)
        {
            this.context = context;
        }

        //Creación de CRUD
        #region
        public void Create(Venta venta)
        {
            context.Ventas.Add(venta);
            context.SaveChanges();

        }

        public Venta Get(int id)
        {

            Venta venta = context.Ventas.FirstOrDefault(e => e.VentaID == id);
            return venta;
        }

        public IList<Venta> GetAll()
        {
            IList<Venta> ventaList = context.Ventas.ToList();
            return ventaList;
        }

        /*public void Update(Venta nuevo)
        {
            Venta oldVenta = Get(nuevo.VentaID);
            oldVenta.ClienteID = nuevo.ClienteID;
            oldVenta.ProductoID = nuevo.ProductoID;
            oldVenta.Cantidad = nuevo.Cantidad;
            oldVenta.MontoVenta = nuevo.MontoVenta;
            oldVenta.FechaVenta = nuevo.FechaVenta;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Venta ventaBorrar = Get(id);
            context.Ventas.Remove(ventaBorrar);
            context.SaveChanges();
        }*/
        #endregion
    }
}
