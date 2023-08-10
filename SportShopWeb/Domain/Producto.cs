using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShopWeb.Domain
{
    [Table("Producto")]
    public class Producto
    {
        [Key, Required]
        public int ProductoID { get; set; }

        [Column("NombreProducto"), Required, MaxLength(50)]
        public string Nombre { get; set; }

        [ForeignKey("TipoProductoID")]
        [Display(Name = "Tipo de producto")]
        public int TipoProductoID { get; set; }

        [Required]
        public string Marca { get; set;}

        [Required]
        public decimal Precio { get; set;}

        [Required]
        public int Stock { get; set;}

        [Column("FechaAltaProducto")]
        [DataType(DataType.Date)]
        public DateTime? FechaAltaProducto { get; set; }

        //Propiedad de navegación: maneja la relación entre clase (Como las relaciones entre tablas en SQL
        public virtual TipoProducto TipoProductoNombre { get; set; }

        public virtual ICollection<Venta> Ventas { get; set; }

        //La propiedad de navegación se debe iniciar en un constructor:

        public Producto()
        {
            Ventas = new List<Venta>();
            //TipoProducto = new TipoProducto();
        }
    }
}
