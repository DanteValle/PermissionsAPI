using PermissionsAPI.CQRS.Commands;
using PermissionsAPI.CQRS.Queries;
using PermissionsAPI.Model;
using PermissionsAPI.Repositories;

namespace PermissionsAPI.Services
{
    public interface IPermissionService
    {
        Task<Permission> RequestPermissionAsync(RequestPermissionCommand command);
        Task<Permission> ModifyPermissionAsync(ModifyPermissionCommand command);
        Task<IEnumerable<Permission>> GetPermissionsAsync(GetPermissionsQuery query);
    }

    public class PermissionService : IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticSearchService _elasticSearchService;

        public PermissionService(IUnitOfWork unitOfWork, IElasticSearchService elasticSearchService)
        {
            _unitOfWork = unitOfWork;
            _elasticSearchService = elasticSearchService;
        }

        public async Task<Permission> RequestPermissionAsync(RequestPermissionCommand command)
        {
            try
            {
                // Crear la entidad Permission
                var permission = new Permission
                {
                    //Id = command.Id,
                    PermissionTypeId = command.PermissionTypeId,
                    NameEmployee = command.NameEmploye,
                    LastNameEmployee = command.LastNameEmployee,
                    PermissionDate = DateTime.UtcNow,
                };

                await _unitOfWork.PermissionRepository.AddAsync(permission);
                await _unitOfWork.SaveAsync();

                // Indexar en Elasticsearch
                await _elasticSearchService.IndexPermissionAsync(permission);

                return permission;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Permission> ModifyPermissionAsync(ModifyPermissionCommand command)
        {
            try
            {
                // Buscar el permiso a modificar
                var permission = await _unitOfWork.PermissionRepository.GetAsync(command.PermissionId);
                if (permission == null)
                    throw new Exception("Permiso no encontrado");

                // Actualizar los campos requeridos
                permission.NameEmployee = command.NameEmploye;
                permission.LastNameEmployee = command.LastNameEmployee;
                permission.PermissionTypeId = command.PermissionTypeId;
                _unitOfWork.PermissionRepository.Update(permission);
                await _unitOfWork.SaveAsync();

                // Actualizar en Elasticsearch (dependiendo de tu lógica se puede indexar de nuevo)
                await _elasticSearchService.IndexPermissionAsync(permission);

                return permission;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync(GetPermissionsQuery query)
        {
            try
            {
                // Aquí podrías aplicar filtros según el query, por simplicidad se retornan todos
                var permissions = await _unitOfWork.PermissionRepository.GetAllAsync();
                // Opcional: Consultar también desde Elasticsearch si se requiere búsqueda avanzada.
                return permissions;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
