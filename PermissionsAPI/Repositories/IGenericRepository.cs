using System.Linq.Expressions;

namespace PermissionsAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    }
}