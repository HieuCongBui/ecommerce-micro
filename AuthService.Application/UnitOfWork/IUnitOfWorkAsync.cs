using AuthService.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.UnitOfWork
{
    public interface IUnitOfWorkAsync: IUnitOfWork,IDisposable
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
        IUnitOfWorkAsync NewUnitOfWorkAsync();
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task CommitAsync();
        Task RollbackAsync();
    }
}
