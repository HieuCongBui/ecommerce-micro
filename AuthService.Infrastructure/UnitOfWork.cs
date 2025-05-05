using AuthService.Application.Repositories;
using AuthService.Application.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;

namespace AuthService.Infrastructure
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        private readonly AuthDbContext _context;
        protected DbTransaction _transaction;
        protected Dictionary<string, dynamic> _repository;
        public UnitOfWork(AuthDbContext context)
        {
            _context = context as AuthDbContext;
            _repository = new Dictionary<string, dynamic>();
        }
        public virtual void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            //throw new NotImplementedException();
        }
        public virtual async Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("Transaction already started. Please commit or rollback the transaction before starting a new one.");
            }
            var connection = _context.Database.GetDbConnection();
            if (connection.State != System.Data.ConnectionState.Open)
            {
                await connection.OpenAsync();
            }
            _transaction = await connection.BeginTransactionAsync(isolationLevel);
        }

        public virtual bool Commit()
        {
            _context.SaveChanges();
            return true;
        }
        public virtual async Task CommitAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction started. Please start a transaction before committing.");
            }
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await DisposeTransactionAsync();
            }
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
        public virtual async Task RollbackAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction started. Please start a transaction before rolling back.");
            }
            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
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

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public virtual void Dispose()
        {
           _transaction?.Dispose();
           _context?.Dispose();
        }
    }
}
