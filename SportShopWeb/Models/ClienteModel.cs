using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportShopWeb.Models
{
    public class ClienteModel
    {
        /*
            El model debe contener todos los datos involucrados en la VISTA
         */

        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido paterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        public string ApellidoMaterno { get; set; }
        public int? Edad { get; set; }

        [Display(Name = "Domicilio")]
        public string Direccion { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }

        [Display(Name = "Fecha de alta")]
        public string FechaAlta { get; set; }

        //Lista para mostrar historial de compras del cliente
        [Display(Name = "Compras del cliente")]
        public IList<VentaModel> historialVentas { get; set; }
    }
}