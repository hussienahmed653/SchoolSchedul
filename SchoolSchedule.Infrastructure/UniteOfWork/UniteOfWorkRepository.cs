using Microsoft.EntityFrameworkCore.Storage;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.UniteOfWork
{
    internal class UniteOfWorkRepository : IUniteOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction? _transaction;

        public UniteOfWorkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            if(_transaction is null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            if(_transaction is not null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackAsync()
        {
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
