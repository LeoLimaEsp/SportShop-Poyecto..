using Microsoft.Data.SqlClient.DataClassification;
using SportShopWeb.Models;

namespace SportShopWeb.Service
{
    public interface IClienteService
    {
        // Definir las operaciones de Servicio
        void Create(ClienteModel cliente);
        void Update(ClienteModel cliente);
        void Delete(int idCliente);
        IList<ClienteModel> GetAll();
        ClienteModel Get(int id);
    }
}