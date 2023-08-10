using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportShopWeb.Models;
using SportShopWeb.Service;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShopWeb.Controllers
{
    public class ClienteController : Controller
    {
        IClienteService clienteService;

        public ClienteController(IClienteService _clienteService)
        {
            clienteService = _clienteService;
        }

        // GET: ClienteController
        public ActionResult Index(string mensaje = "")
        {
            ViewData["mensaje"] = mensaje;
            try
            {
                IList<ClienteModel> listaClientes = clienteService.GetAll();
                return View(listaClientes);
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return View("Error", new ErrorViewModel() { Message = ex.Message });
            }
        }

        // GET: ClienteController/Details/
        public ActionResult Details(int id)
        {
            try
            {
                ClienteModel cliente = clienteService.Get(id);
                return View(cliente);
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
                ClienteModel clienteModel = new ClienteModel();
                return View(clienteModel);
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
        public ActionResult Create(ClienteModel clienteModel)
        {
            try
            {
                // Guardar el clienteModel
                clienteService.Create(clienteModel);
                return RedirectToAction("Index", new { mensaje = "¡Nuevo cliente agregado con éxito!" });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                //return View("Error", new ErrorViewModel() { Message = ex.Message });
                return RedirectToAction("Index", new { mensaje = "No se pudo crear un nuevo cliente en este momento." });
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                ClienteModel clienteActual = clienteService.Get(id);
                return View(clienteActual);
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
        public ActionResult Edit(ClienteModel clienteModel)
        {
            try
            {
                clienteService.Update(clienteModel);
                return RedirectToAction("Index", new { mensaje = "¡El cliente se editó con éxito!" });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo editar el cliente en este momento." });
            }
        }

        // GET: ClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                clienteService.Delete(id);
                return RedirectToAction("Index", new { mensaje = "¡El cliente se eliminó con éxito!" });
            }
            catch (ApplicationException ex)
            {
                return RedirectToAction("Index", new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                // Dos formas de manejar errores: Vista, Mensaje al Index
                return RedirectToAction("Index", new { mensaje = "No se pudo eliminar el cliente en este momento." });
            }
        }

    }
}