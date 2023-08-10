﻿using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;
using SportShopWeb.Models;
using SportShopWeb.Utils;

namespace SportShopWeb.Service
{
    public class ClienteService : IClienteService
    {
        // 1.- Objecto que lo conecte con el repository: UnitOfWork
        IUnitOfWork uow;

        // Inyectarlo
        public ClienteService(IUnitOfWork _uow) { 
            uow = _uow;
        }

        // 2.- Implementar todos los métodos descritos en la Interfaz
        public void Create(ClienteModel cliente)
        {
            Cliente clienteDomain = new Cliente();
            // Transformar el Model en Domain
            clienteDomain.Nombre = cliente.Nombre;
            clienteDomain.ApellidoPaterno = cliente.ApellidoPaterno;
            clienteDomain.ApellidoMaterno = cliente.ApellidoMaterno;
            clienteDomain.Direccion = cliente.Direccion;
            clienteDomain.Edad = cliente.Edad;
            clienteDomain.Estado = cliente.Estado;
            clienteDomain.Municipio = cliente.Municipio;
            clienteDomain.FechaAlta = DateTime.Now;

            // Hacer transacciones
            try
            {
                // Guardar el cliente
                uow.ClienteRepository.Create(clienteDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Delete(int idCliente)
        {
            try
            {
                //Verificar si el cliente participa en alguna venta, siendo asi no se puede borrar.
                Cliente cliente = uow.ClienteRepository.Get(idCliente);
                IList<Venta> ventasList = cliente.Ventas.ToList();
                if (ventasList.Count > 0)
                {
                    throw new ApplicationException("Advertencia. Cliente no se puede borrar porque se encuentra asociado a al menos una venta.");
                }

            
                uow.ClienteRepository.Delete(idCliente);
                uow.Commit();
            
            }
            catch (Exception ex)
            {
                uow.Rollback();
                throw ex;
            }           
        }

        public ClienteModel Get(int id)
        {
            try
            {
                Cliente clienteDomain = uow.ClienteRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                ClienteModel clienteModel = new ClienteModel()
                {
                    Id = clienteDomain.ClienteId,
                    Nombre = clienteDomain.Nombre,
                    ApellidoPaterno = clienteDomain.ApellidoPaterno,
                    ApellidoMaterno = clienteDomain.ApellidoMaterno,
                    Direccion = clienteDomain.Direccion,
                    Edad = clienteDomain.Edad,
                    Estado = clienteDomain.Estado,
                    Municipio = clienteDomain.Municipio,
                    FechaAlta = Util.DateToString(clienteDomain.FechaAlta)
                };

                //Se extrae y se asigna a una lista el historial de ventas hechas por un cliente.
                //Usando propiedades de navegación.
                clienteModel.historialVentas = clienteDomain.Ventas.Select(v => new VentaModel()
                {
                    productoNombre = v.ProductoNombre.Nombre,
                    MontoVenta = v.MontoVenta,
                    FechaVenta = Util.DateToString(v.FechaVenta),
                    Cantidad = v.Cantidad
                }).ToList(); 

                return clienteModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public IList<ClienteModel> GetAll()
        {
            try
            {
                var query = uow.ClienteRepository.GetAll();
                
                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<ClienteModel> listaClientes = query.Select(c => new ClienteModel(){
                        Id = c.ClienteId,
                        Nombre = c.Nombre,
                        ApellidoPaterno = c.ApellidoPaterno,
                        ApellidoMaterno = c.ApellidoMaterno,
                        Direccion = c.Direccion,
                        Edad = c.Edad,
                        Estado = c.Estado,
                        Municipio = c.Municipio,
                        FechaAlta = Util.DateToString(c.FechaAlta)
                }).ToList();

                return listaClientes;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
            
        }

        public void Update(ClienteModel cliente)
        {
            Cliente clienteDomain = new Cliente
            {
                // Transformar el Model en Domain
                ClienteId = cliente.Id,   // <--- AQUI si se usa el Id
                Nombre = cliente.Nombre,
                ApellidoPaterno = cliente.ApellidoPaterno,
                ApellidoMaterno = cliente.ApellidoMaterno,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Estado = cliente.Estado,
                Municipio = cliente.Municipio,
                FechaAlta = Util.StringToDate(cliente.FechaAlta)
            };
            // Hacer transacciones
            try
            {
                // Actualizar el cliente por medio del Repository
                uow.ClienteRepository.Update(clienteDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }
    }
}