using Microsoft.AspNetCore.Mvc.Rendering;
using SportShopWeb.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShopWeb.Models
{
    public class ProductoModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Deportes")]
        public int TipoProductoID { get; set; }

        [Display(Name = "Deporte")]
        public string TipoProductoNombre { get; set;}

        [Required, MaxLength(50)]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        [Display(Name = "Cantidad disponible")]
        public int Stock { get; set; }

        [Display(Name = "Fecha de alta")]
        public string FechaAltaProducto { get; set; }

        // Lista de Items (parejas: valor-texto)
        public IEnumerable<SelectListItem> tiposProducto { get; set; }
    }
}