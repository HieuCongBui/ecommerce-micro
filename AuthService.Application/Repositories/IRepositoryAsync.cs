using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Repositories
{
    public interface IRepositoryAsync<TEntity> :IRepository<TEntity> where TEntity: class
    {
        Task<TEntity> FindAsync(object Id);
        Task<TEntity> FindAsync(CancellationToken cancellationToken,params object[] keyValues);
        Task<bool> DeleteAsync(object id);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
