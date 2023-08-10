using Microsoft.AspNetCore.Mvc;
using SportShopWeb.Models;
using SportShopWeb.Service;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;

namespace SportShopWeb.Controllers
{
    public class VentaController : Controller
    {
        IVentaService ventaService;

        public VentaController(IVentaService _ventaService)
        {
            ventaService = _ventaService;
        }

        // GET: VentaController
        public ActionResult Index(string mensaje = "NA")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<VentaModel> listaVentas = ventaService.GetAll();
                return View(listaVentas);
              
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
                VentaModel venta = ventaService.Get(id);
                return View(venta);
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
                return View(ventaService.Get());
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
        public ActionResult Create(VentaModel ventaModel)
        {
            try
            {
                // Guardar el clienteModel
                ventaService.Create(ventaModel);
                return RedirectToAction("Index", new {mensaje = "¡Venta exitosa!"});
            }
            catch(ApplicationException ex)
            {
                return RedirectToAction("Index", new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                //return View("Error", new ErrorViewModel() { Message = ex.Message });
                return RedirectToAction("Index", new { mensaje = "No se pudo realizar la venta, intente mas tarde." });
            }
        }

        // GET: ClienteController/Edit/
        /*public ActionResult Edit(int id)
        {
            VentaModel ventaActual = ventaService.Get(id);
            return View(ventaActual);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VentaModel ventaModel)
        {
            try
            {
                ventaService.Update(ventaModel);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                ventaService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Aqui se debe mostrar un mensaje de error (lo vamos agregaremos al final)
                throw;
            }
        }}*/
    }
}
