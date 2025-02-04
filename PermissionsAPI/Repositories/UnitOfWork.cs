using PermissionsAPI.DataAccess;
using PermissionsAPI.Infrastructure;
using PermissionsAPI.Model;

namespace PermissionsAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<Permission> _permissionRepository;
        private IGenericRepository<PermissionType> _permissionTypeRepository;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Permission> PermissionRepository =>
            _permissionRepository ??= new GenericRepository<Permission>(_context);

        public IGenericRepository<PermissionType> PermissionTypeRepository =>
            _permissionTypeRepository ??= new GenericRepository<PermissionType>(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
