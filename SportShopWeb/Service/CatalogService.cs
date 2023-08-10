using SportShopWeb.Data.Infrastructure;
using SportShopWeb.Domain;
using SportShopWeb.Models;
using SportShopWeb.Utils;

namespace SportShopWeb.Service
{
    public class CatalogService : ICatalogService
    {
        // 1.- Objecto que lo conecte con el repository: UnitOfWork
        IUnitOfWork uow;

        // Inyectarlo
        public CatalogService(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        //Get
        public void Create(TipoProductoModel tipoProductoModel)
        {
            TipoProducto tipoProductoDomain = new TipoProducto();
            // Transformar el Model en Domain
            tipoProductoDomain.Nombre = tipoProductoModel.NombreTipoProducto;
            tipoProductoDomain.FechaAlta = DateTime.Now;
           
            // Hacer transacciones
            try
            {
                // Guardar el cliente
                uow.TipoProductoRepository.Create(tipoProductoDomain);
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Delete(int IdTipoProducto)
        {
            try
            {           
                uow.TipoProductoRepository.Delete(IdTipoProducto);
                uow.Commit();

            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public TipoProductoModel Get(int id)
        {
            try
            {
                TipoProducto tipoProductoDomain = uow.TipoProductoRepository.Get(id);
                // Transformamos el Domain en Model (para poder retornarselo al controller)
                TipoProductoModel tipoProductoModel = new TipoProductoModel()
                {
                    Id = tipoProductoDomain.TipoProductoID,
                    NombreTipoProducto = tipoProductoDomain.Nombre,
                    FechaAltaTipoProducto = Util.DateToString(tipoProductoDomain.FechaAlta)
                };

                return tipoProductoModel;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public IList<TipoProductoModel> GetAll()
        {
            try
            {
                var query = uow.TipoProductoRepository.GetAll();

                // Transformar cada 'Domain' de la colección query a una lista de 'Model'
                IList<TipoProductoModel> listatiposProducto = query.Select(c => new TipoProductoModel()
                {
                    Id = c.TipoProductoID,
                    NombreTipoProducto = c.Nombre,
                    FechaAltaTipoProducto = Util.DateToString(c.FechaAlta)
                }).ToList();

                return listatiposProducto;
            }
            catch (Exception)
            {
                uow.Rollback();
                throw;
            }
        }

        public void Update(TipoProductoModel tipoProductoModel)
        {
            TipoProducto tipoProductoDomain = new TipoProducto
            {
                // Transformar el Model en Domain
                TipoProductoID = tipoProductoModel.Id,   // <--- AQUI si se usa el Id
                Nombre = tipoProductoModel.NombreTipoProducto,
                FechaAlta = Util.StringToDate(tipoProductoModel.FechaAltaTipoProducto)
            };
            // Hacer transacciones
            try
            {
                // Actualizar el cliente por medio del Repository
                uow.TipoProductoRepository.Update(tipoProductoDomain);
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
