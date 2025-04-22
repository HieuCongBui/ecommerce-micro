using AuthService.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AuthService.Infrastructure
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        private readonly AuthDbContext _context;
        private DbTransaction _transaction;
        protected Dictionary<string, dynamic> Repositories;
        public UnitOfWork(AuthDbContext context)
        {
            _context = context as AuthDbContext;
            Repositories = new Dictionary<string, dynamic>();
        }
        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            //throw new NotImplementedException();
        }

        public virtual bool Commit()
        {
            _context.SaveChanges();
            return true;
        }

        public virtual int ExcuteSqlCommand(string sql, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRaw(sql, parameters);
        }

        public virtual Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken, params object[] parameters)
        {
            return _context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken: cancellationToken);
        }

        public virtual Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
           return _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public virtual IUnitOfWorkAsync NewUnitOfWorkAsync()
        {
            return new UnitOfWork(_context);
        }

        public virtual void Rollback()
        {
            _transaction.Rollback();
            _context.Dispose();
        }

        public virtual int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
