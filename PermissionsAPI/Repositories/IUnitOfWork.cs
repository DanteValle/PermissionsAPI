using PermissionsAPI.Model;

namespace PermissionsAPI.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Permission> PermissionRepository { get; }
        IGenericRepository<PermissionType> PermissionTypeRepository { get; }
        Task<int> SaveAsync();
    }
}