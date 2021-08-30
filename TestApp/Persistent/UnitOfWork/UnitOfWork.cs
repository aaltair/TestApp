using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace TestApp.Persistent.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        #region Fields

        private bool _disposed;
        protected TContext _context;

        #endregion

        public UnitOfWork(TContext context)
        {
            _context = context;
        }


        public void Commit()
        {
            _context.SaveChanges();
        }


        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void DetachAll()
        {
            foreach (EntityEntry entityEntry in this._context.ChangeTracker.Entries().ToArray())
            {
                if (entityEntry.Entity != null)
                {
                    entityEntry.State = EntityState.Detached;
                }
            }
        }

        public async Task<IDbContextTransaction> CreateTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task RollBackTransactionAsync(IDbContextTransaction tran)
        {
            await tran.RollbackAsync();
        }

        public async Task CommitTransactionAsync(IDbContextTransaction tran)
        {
            await tran.CommitAsync();
        }

        public IDbContextTransaction GetContextTransaction()
        {
            return _context.Database.CurrentTransaction;
        }

        public void Dispose()
        {
            _disposed = true;
            _context.Dispose();
        }
    }
}