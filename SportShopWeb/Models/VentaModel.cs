using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace SportShopWeb.Models
{
    public class VentaModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Clientes")]
        public int ClienteID { get; set; }

        [Display(Name = "Cliente")]
        public string clienteNombre { get; set; }

        [Display(Name = "Productos")]
        public int ProductoID { get; set; }

        [Display(Name = "Producto")]
        public string productoNombre { get; set; }

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Display(Name = "$ Monto total")]
        public decimal MontoVenta { get; set; }

        [Display(Name = "Fecha de venta")]
        public string FechaVenta { get; set; }

        // Lista de Items (parejas: valor-texto)
        public IEnumerable<SelectListItem> Productos { get; set; }
        public IEnumerable<SelectListItem> Clientes { get; set; }
        

    }
}
