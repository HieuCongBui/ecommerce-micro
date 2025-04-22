using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.UnitOfWork
{
    public interface IUnitOfWorkAsync: IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters);
        IUnitOfWorkAsync NewUnitOfWorkAsync();
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
    }
}
