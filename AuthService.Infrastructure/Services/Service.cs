using AuthService.Application.IServices;
using AuthService.Application.Repositories;

namespace AuthService.Infrastructure.Services
{
    public abstract class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        protected readonly IRepositoryAsync<TEntity> _repository;
        protected Service(IRepositoryAsync<TEntity> repositor)
        {
            _repository = repositor;
        }
        public virtual void Delete(object id) => _repository.Delete(id);

        public virtual Task<bool> DeleteAsync(object id) => _repository.DeleteAsync(id);

        public virtual Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues) => _repository.DeleteAsync(cancellationToken, keyValues);

        public virtual TEntity Find(object Id) => _repository.Find(Id);

        public virtual TEntity Find(params object[] keyValues) => _repository.Find(keyValues);

        public virtual Task<TEntity> FindAsync(object Id) => _repository.FindAsync(Id);

        public virtual Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues) => _repository.FindAsync(cancellationToken, keyValues);

        public virtual void Insert(TEntity entity) => _repository.Insert(entity);

        public virtual void InsertRange(IEnumerable<TEntity> entities) => _repository.InsertRange(entities);

        public virtual IQueryable<TEntity> Queryable() => _repository.Queryable();

        public virtual void Update(TEntity entity) => _repository.Update(entity);
    }
}
