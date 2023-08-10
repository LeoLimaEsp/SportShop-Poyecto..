using Microsoft.AspNetCore.Mvc.Rendering;
using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;
using SportShopWeb.Models;
using SportShopWeb.Utils;
namespace SportShopWeb.Service
{
    public class ProductoService : IProductoService
    {
        // 1.- Objecto que lo conecte con el repository: UnitOfWork
        IUnitOfWork uow;

        // Inyectarlo
        public ProductoService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        // 2.- Implementar todos los métodos descritos en la Interfaz
        public void Create(ProductoModel productoModel)
        {
            Producto productoDomain = new Producto();
            // Transformar el Model en Domain
            productoDomain.Nombre = productoModel.Nombre;
            productoDomain.TipoProductoID = productoModel.TipoProductoID;
            productoDomain.Precio = productoModel.Precio;
            productoDomain.Marca = productoModel.Marca;
            productoDomain.Stock = productoModel.Stock;
            productoDomain.FechaAltaProducto = DateTime.Now;


            // Hacer transacciones
            try
            {
                // Guardar el cliente
                uow.ProductoRepository.Create(productoDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Delete(int idProducto)
        {
            try
            {
                //Verificar si el producto participa en alguna venta, siendo asi no se puede borrar.
                Producto producto = uow.ProductoRepository.Get(idProducto);
                IList<Venta> ventasList = producto.Ventas.ToList();
                if(ventasList.Count > 0) 
                {
                    throw new ApplicationException("Advertencia. Producto no se puede borrar porque se encuentra asociado a al menos una venta.");
                }

                uow.ProductoRepository.Delete(idProducto);
                uow.Commit();
            }
            catch (Exception ax)
            {
                uow.Rollback();
                throw ax;
            }
        }

        //Get para un solo producto completo
        public ProductoModel Get(int id)
        {
            try
            {
                Producto productoDomain = uow.ProductoRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                ProductoModel productoModel = new ProductoModel()
                {
                    Id = productoDomain.ProductoID,
                    Nombre = productoDomain.Nombre,
                    Precio = productoDomain.Precio,
                    Marca = productoDomain.Marca,
                    TipoProductoNombre = productoDomain.TipoProductoNombre.Nombre,
                    Stock = productoDomain.Stock,
                    TipoProductoID = productoDomain.TipoProductoID,
                    FechaAltaProducto = Util.DateToString(productoDomain.FechaAltaProducto)
                };

                //Lista de tipos de productos para editar se muestre la lista
                var listaTipos = uow.TipoProductoRepository.GetAll();

                productoModel.tiposProducto = listaTipos.Select(t => new SelectListItem()
                {
                    Value = t.TipoProductoID.ToString(),
                    Text = t.Nombre
                }).ToList();

                return productoModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        //Get para todos los productos
        public IList<ProductoModel> GetAll()
        {
            try
            {
                var query = uow.ProductoRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<ProductoModel> listaProductos = query.Select(p => new ProductoModel()
                {
                    Id = p.ProductoID,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Marca = p.Marca,
                    Stock = p.Stock,
                    TipoProductoID = p.TipoProductoID,
                    TipoProductoNombre = p.TipoProductoNombre.Nombre, //Se usa propiedad de navegación para acceder a datos de otro objeto (JOIN).
                    FechaAltaProducto = Util.DateToString(p.FechaAltaProducto)
                }).ToList();

                return listaProductos;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }

        }

        //Get para obtener los tipos de productos
        public ProductoModel Get()
        {
            try
            {
                ProductoModel productoModel = new ProductoModel();

                var listaTipos = uow.TipoProductoRepository.GetAll();

                productoModel.tiposProducto = listaTipos.Select(t => new SelectListItem()
                {
                    Value = t.TipoProductoID.ToString(),
                    Text = t.Nombre
                }).ToList();

                return productoModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }


        public void Update(ProductoModel producto)
        {
            Producto productoDomain = new Producto
            {
                // Transformar el Model en Domain
                ProductoID = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Marca = producto.Marca,
                Stock = producto.Stock,
                TipoProductoID = producto.TipoProductoID,
                //FechaAltaProducto = Util.StringToDate(producto.FechaAltaProducto)
            };
            // Hacer transacciones
            try
            {
                // Actualizar el cliente por medio del Repository
                uow.ProductoRepository.Update(productoDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

    }  
}
