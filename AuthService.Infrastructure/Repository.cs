using AuthService.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity>, IRepositoryAsync<TEntity> where TEntity : class
    {
        private AuthDbContext _context;
        private DbSet<TEntity> _dbSet;
        public Repository(AuthDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public virtual TEntity Find(object Id)
        {
            return _dbSet.Find(Id);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(object Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual void Insert(TEntity entity) => _dbSet.Add(entity);

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _dbSet.Add(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void delete(object id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public virtual Task<bool> DeleteAsync(object id)
        {
            return DeleteAsync(CancellationToken.None, id);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await _dbSet.FindAsync(keyValues);
            if (entity == null) return false;
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return true;
        }

        public IQueryable<TEntity> Queryable() => _dbSet;
    }
}
