using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;

namespace SportShopWeb.Data.Repository
{
    public class ProductoRepository
    {
        /*
       Las clases tipo repository contienen las operaciones a la tabla correspondiente (VENTA).
       Tipicamente el CRUD + unas consultas personalizadas. 
       Todas las operaciones se realizan mediante el context. 
       */

        SportShopContext context; //Objeto vacio, se requiere inyeccion

        //Inyección del objeto: creando un constructor
        public ProductoRepository(SportShopContext context)
        {
            this.context = context;
        }

        //Creación de CRUD
        #region
        public void Create(Producto producto)
        {
            context.Productos.Add(producto);
            context.SaveChanges();
        }

        public Producto Get(int id)
        {
            Producto producto = context.Productos.FirstOrDefault(e => e.ProductoID == id);
            return producto;
        }

        public IList<Producto> GetAll()
        {
            IList<Producto> productoList = context.Productos.ToList();
            return productoList;
        }

        public void Update(Producto nuevo)
        {
            Producto oldProducto = Get(nuevo.ProductoID);
            oldProducto.Nombre = nuevo.Nombre;
            oldProducto.TipoProductoID = nuevo.TipoProductoID;
            oldProducto.Marca = nuevo.Marca;
            oldProducto.Precio = nuevo.Precio;
            oldProducto.Stock = nuevo.Stock;
            //oldProducto.FechaAltaProducto = nuevo.FechaAltaProducto;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Producto productoBorrar = Get(id);
            context.Productos.Remove(productoBorrar);
            context.SaveChanges();
        }
        #endregion
    }
}
