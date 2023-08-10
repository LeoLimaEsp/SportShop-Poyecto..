using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShopWeb.Domain
{
    [Table("TipoProducto")]
    public class TipoProducto
    {
        [Key]
        public int TipoProductoID { get; set; }

        [Column("NombreTipoProducto")]
        public string Nombre { get; set; }

        [Column("FechaAltaTipoProducto")]
        public DateTime? FechaAlta { get; set; }

        //Propiedad de navegación:
        public virtual ICollection<Producto> Productos { get; set; }
        //public virtual ICollection<Venta> Ventas { get; set; }//***

        //La propiedad de navegación se debe iniciar en un constructor:

        public TipoProducto()
        {
            Productos = new List<Producto>();
            //Ventas = new List<Venta>();
        }
    }
}
