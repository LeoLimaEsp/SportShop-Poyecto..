using Microsoft.AspNetCore.Mvc;
using SportShopWeb.Models;
using SportShopWeb.Service;

namespace SportShopWeb.Controllers
{
    public class CatalogController : Controller
    {
        ICatalogService tipoProductoService;

        public CatalogController(ICatalogService _tipoProductoService)
        {
            tipoProductoService = _tipoProductoService;
        }

        // GET: ClienteController
        public ActionResult Index(string mensaje = "")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<TipoProductoModel> listaTipoProducto = tipoProductoService.GetAll();
                return View(listaTipoProducto);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                TipoProductoModel tipoProducto = tipoProductoService.Get(id);
                return View(tipoProducto);
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
                TipoProductoModel tipoProductoModel = new TipoProductoModel();
                return View(tipoProductoModel);
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
        public ActionResult Create(TipoProductoModel tipoProductoModel)
        {
            try
            {
                // Guardar el clienteModel
                tipoProductoService.Create(tipoProductoModel);
                return RedirectToAction("Index", new { mensaje = "¡Nuevo deporte agregado con éxito!" });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                //return View("Error", new ErrorViewModel() { Message = ex.Message });
                return RedirectToAction("Index", new { mensaje = "No se pudo agregar un nuevo deporte en este momento." });
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                TipoProductoModel tipoProductoActual = tipoProductoService.Get(id);
                return View(tipoProductoActual);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoProductoModel tipoProductoModel)
        {
            try
            {
                tipoProductoService.Update(tipoProductoModel);
                return RedirectToAction("Index", new { mensaje = "¡El deporte se editó con éxito." });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo editar el deporte en este momento." });
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                tipoProductoService.Delete(id);
                return RedirectToAction("Index", new { mensaje = "¡El deporte se eliminó con éxito." });
            }          
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "Advertencia. El deporte no se puede borrar porque se encuentra asociado a al menos una venta." });
            }
        }
    }
}
