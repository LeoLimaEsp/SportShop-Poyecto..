using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShopWeb.Domain
{
    [Table("Venta")]
    public class Venta
    {
        //Propiedades escalares (columnas de tablas)
        [Key]
        public int VentaID { get; set; }

        [ForeignKey("ClienteID")]
        public int ClienteID { get; set; }

        [ForeignKey("ProductoID")]
        public int ProductoID { get; set; }

        public int Cantidad { get; set;}

        public decimal MontoVenta { get; set;}

        [Column("FechaVenta")]
        [DataType(DataType.Date)]
        public DateTime? FechaVenta { get; set; }

        //Propiedades de navegación: sirve para relacionar las clases
        //Foreign key: sirve para relacionar tablas

        public virtual Cliente ClienteNombre { get; set; } 
        public virtual Producto ProductoNombre { get; set; }
        //public virtual TipoProducto catalog { get; set; }
        
    }
}
