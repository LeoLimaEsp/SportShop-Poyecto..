using Microsoft.AspNetCore.Mvc;
using SportShopWeb.Models;
using SportShopWeb.Service;

namespace SportShopWeb.Controllers
{
    public class ProductoController : Controller
    {
        IProductoService productoService;

        public ProductoController(IProductoService _productoService)
        {
            productoService = _productoService;
        }

        // GET: ClienteController
        public ActionResult Index(string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<ProductoModel> listaProductos = productoService.GetAll();
                return View(listaProductos);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id, string mensaje = "")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                ProductoModel producto = productoService.Get(id);
                return View(producto);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            try
            {
                //Se crea un model vacion CON la lista de tipos de producto
                return View(productoService.Get());
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductoModel productoModel)
        {
            try
            {
                // Guardar el ProductoModel
                productoService.Create(productoModel);
                return RedirectToAction("Index", new { mensaje = "¡Nuevo producto agregado con éxito!" });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                //return View("Error", new ErrorViewModel() { Message = ex.Message });
                return RedirectToAction("Index", new { mensaje = "No se pudo crear un nuevo producto." });
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            { 
            ProductoModel productoActual = productoService.Get(id);
            return View(productoActual);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo editar el producto en este momento." });
            }
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductoModel productoModel)
        {
            try
            {
                productoService.Update(productoModel);
                return RedirectToAction("Details", new { id=productoModel.Id, mensaje = "El producto se editó con éxito." });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo editar el producto en este momento." });
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                productoService.Delete(id);
                return RedirectToAction("Index", new { mensaje = "El producto se eliminó con éxito." });
            }
            catch (ApplicationException ax)
            {
                return RedirectToAction("Index", new { mensaje = ax.Message});
            }
            catch (Exception ax)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo eliminar el producto en este momento." });
            }
        }
    }
}
