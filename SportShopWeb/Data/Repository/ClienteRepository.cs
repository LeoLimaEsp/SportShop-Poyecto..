using Microsoft.EntityFrameworkCore.Diagnostics;
using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;

namespace SportShopWeb.Data.Repository
{
    public class ClienteRepository
    {
        /*
        Las clases tipo repository contienen las operaciones a la tabla correspondiente (CLIENTE).
        Tipicamente el CRUD + unas consultas personalizadas. 
        Todas las operaciones se realizan mediante el context. 
        */

        SportShopContext context; //Objeto vacio, se requiere inyeccion

        //Inyección del objeto: creando un constructor
        public ClienteRepository (SportShopContext context)
        {
            this.context = context;
        }

        //Creación de CRUD
        #region
        public void  Create(Cliente cliente)
        {
            context.Clientes.Add(cliente);
            context.SaveChanges();
        }

        public Cliente Get(int id)
        {
            Cliente cliente = context.Clientes.FirstOrDefault(e => e.ClienteId == id);
            return cliente;
        }

        public IQueryable<Cliente>GetAll()
        {
            var clienteList = context.Clientes.Select( c => c);
            return clienteList;
        }

        public void Update(Cliente nuevo)
        {
            Cliente oldCliente = Get(nuevo.ClienteId);
            oldCliente.Nombre = nuevo.Nombre;
            oldCliente.ApellidoPaterno = nuevo.ApellidoPaterno;
            oldCliente.ApellidoMaterno = nuevo.ApellidoMaterno;
            oldCliente.Edad = nuevo.Edad;
            oldCliente.Direccion = nuevo.Direccion;
            oldCliente.Estado = nuevo.Estado;
            oldCliente.Municipio = nuevo.Municipio;
            //oldCliente.FechaAlta = nuevo.FechaAlta;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Cliente clienteBorrar = Get(id);
            context.Clientes.Remove(clienteBorrar);
        }
        #endregion
    }
}
