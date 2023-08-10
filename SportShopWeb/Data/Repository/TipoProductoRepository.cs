using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;

namespace SportShopWeb.Data.Repository
{
    public class TipoProductoRepository
    {
        /*
      Las clases tipo repository contienen las operaciones a la tabla correspondiente (VENTA).
      Tipicamente el CRUD + unas consultas personalizadas. 
      Todas las operaciones se realizan mediante el context. 
      */

        SportShopContext context; //Objeto vacio, se requiere inyeccion

        //Inyección del objeto: creando un constructor
        public TipoProductoRepository(SportShopContext context)
        {
            this.context = context;
        }

        //Creación de CRUD
        #region
        public void Create(TipoProducto tipoProducto)
        {
            context.TipoProductos.Add(tipoProducto);
            context.SaveChanges();

        }

        public TipoProducto Get(int id)
        {

            TipoProducto tipoProducto = context.TipoProductos.FirstOrDefault(e => e.TipoProductoID == id);
            return tipoProducto;
        }

        public IList<TipoProducto> GetAll()
        {
            IList<TipoProducto> tipoProductoList = context.TipoProductos.ToList();
            return tipoProductoList;
        }

        public void Update(TipoProducto nuevo)
        {
            TipoProducto oldTipoProducto = Get(nuevo.TipoProductoID);
            oldTipoProducto.Nombre = nuevo.Nombre;
            
            //oldTipoProducto.FechaAlta = nuevo.FechaAlta;
            
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            TipoProducto tipoProductoBorrar = Get(id);
            context.TipoProductos.Remove(tipoProductoBorrar);
            context.SaveChanges();
        }
        #endregion
    }
}
