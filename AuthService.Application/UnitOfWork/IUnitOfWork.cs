using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AuthService.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        int ExcuteSqlCommand(string sql, params object[] parameters);
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}
