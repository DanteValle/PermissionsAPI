﻿using Microsoft.EntityFrameworkCore;
using PermissionsAPI.DataAccess;
using PermissionsAPI.Infrastructure;
using System.Linq.Expressions;

namespace PermissionsAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILog _log;

        public GenericRepository(AppDbContext context,ILog log)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _log = log;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _log.Exeption("ERROR",ex);
                throw;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                _log.Exeption("ERROR", ex);
                throw;
            }
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);

            }
            catch (Exception ex)
            {
                _log.Exeption("ERROR", ex);
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);

            }
            catch (Exception ex)
            {
                _log.Exeption("ERROR", ex);
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);

            }
            catch (Exception ex)
            {
                _log.Exeption("ERROR", ex);
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _dbSet.Where(predicate).ToListAsync();

            }
            catch (Exception ex)
            {
                _log.Exeption("ERROR", ex);
                throw;
            }
        }
    }
}
