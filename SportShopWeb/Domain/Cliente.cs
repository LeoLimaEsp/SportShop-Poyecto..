using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace SportShopWeb.Domain
{
    [Table("Cliente")]
    public class Cliente
    {
        
        [Key, Required]
        public int ClienteId { get; set; }

        [Column("NombreCliente")]
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int? Edad { get; set; }
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }

        [Column("FechaAltaCliente")]
        [DataType(DataType.Date)]
        public DateTime? FechaAlta { get; set; }

        // Propiedades de navegación
        public virtual ICollection<Venta> Ventas { get; set; }

        public Cliente()
        {
            Ventas = new List<Venta>();
        }

    }
}