using SportShopWeb.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SportShopWeb.Models
{
    public class TipoProductoModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Nombre del deporte")]
        public string NombreTipoProducto { get; set; }


        [Display(Name = "Fecha de alta del deporte")]
        public string FechaAltaTipoProducto { get; set; }

    }
}