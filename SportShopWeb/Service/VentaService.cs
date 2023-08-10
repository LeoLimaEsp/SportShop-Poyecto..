using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;
using SportShopWeb.Models;
using SportShopWeb.Utils;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Identity.Client;

namespace SportShopWeb.Service
{
    public class VentaService : IVentaService
    {
        // 1.- Objecto que lo conecte con el repository: UnitOfWork
        IUnitOfWork uow;

        // Inyectarlo
        public VentaService(IUnitOfWork _uow)
        {
            uow = _uow;
        }
        public void Create(VentaModel ventaModel)
        {
            // Hacer transacciones
            try
            {
                //TO-DO: Validar que me alcance el stock.
                //Calcular el monto total de venta.
                Producto producto = uow.ProductoRepository.Get(ventaModel.ProductoID);

                if ((ventaModel.Cantidad <= producto.Stock) && (producto.Stock != 0))
                {
                    //Restar la cantidad vendida al stock del producto.

                    producto.Stock -= ventaModel.Cantidad;
                    decimal montoVenta = producto.Precio * ventaModel.Cantidad;

                    Venta ventaDomain = new Venta();
                    // Transformar el Model en Domain
                    ventaDomain.ClienteID = ventaModel.ClienteID;
                    ventaDomain.Cantidad = ventaModel.Cantidad;
                    ventaDomain.MontoVenta = montoVenta;
                    ventaDomain.FechaVenta = DateTime.Now;
                    ventaDomain.ProductoID = ventaModel.ProductoID;

                    // Guardar el cliente y actualizar los datos de la base de datos.  
                    uow.ProductoRepository.Update(producto); // Actualizar la BDD.
                    uow.VentaRepository.Create(ventaDomain); //Se guarda el registro de la venta.

                    //Confirmar transacción.
                    uow.Commit();
                }
                else
                {
                    throw new ApplicationException("No fue posible realizar la compra por cantidad insuficiente de producto, intente con una cantidad menor. \nStock disponible: " + producto.Stock);
                }
                
               
            }
            catch (Exception ex)
            {
                uow.Rollback();
                throw ex;
            }
        }

        public VentaModel Get(int id)
        {
            try
            {
                Venta ventaDomain = uow.VentaRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                VentaModel VentaModel = new VentaModel()
                {
                    Id = ventaDomain.VentaID,
                    ClienteID = ventaDomain.ClienteID,
                    clienteNombre = ventaDomain.ClienteNombre.Nombre + " " + ventaDomain.ClienteNombre.ApellidoMaterno + " " + ventaDomain.ClienteNombre.ApellidoPaterno,
                    Cantidad = ventaDomain.Cantidad,
                    MontoVenta = ventaDomain.MontoVenta,
                    FechaVenta = Util.DateToString(ventaDomain.FechaVenta),
                    ProductoID = ventaDomain.ProductoID,
                    productoNombre = ventaDomain.ProductoNombre.Nombre + "    Marca: " + ventaDomain.ProductoNombre.Marca
                };

                return VentaModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public IList<VentaModel> GetAll()
        {
            try
            {
                var query = uow.VentaRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<VentaModel> listaVentas = query.Select(c => new VentaModel()
                {
                    Id = c.VentaID,
                    ProductoID = c.ProductoID,
                    productoNombre = c.ProductoNombre.Nombre + "   Marca: " + c.ProductoNombre.Marca,
                    ClienteID = c.ClienteID,
                    clienteNombre = c.ClienteNombre.Nombre + " " + c.ClienteNombre.ApellidoMaterno,
                    Cantidad = c.Cantidad,
                    MontoVenta = c.MontoVenta,
                    FechaVenta = Util.DateToString(c.FechaVenta)
                }).ToList();

                return listaVentas;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        //Get para obtener los tipos de productos
        public VentaModel Get()
        {
            try
            {
                VentaModel ventaModel = new VentaModel();

                var clienteTipos = uow.ClienteRepository.GetAll();

                ventaModel.Clientes = clienteTipos.Select(t => new SelectListItem()
                {
                    Value = t.ClienteId.ToString(),
                    Text =String.Format("{0} {1}", t.Nombre, t.ApellidoPaterno)
                });

                var productoTipos = uow.ProductoRepository.GetAll();

                ventaModel.Productos = productoTipos.Select(t => new SelectListItem()
                {
                    Value = t.ProductoID.ToString(),
                    Text =String.Format("{0} {1}", t.Nombre, t.Marca)
                });

                return ventaModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        
    }
}
