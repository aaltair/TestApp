using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Persistent
{
  
        public interface IUnitOfWork<T> where T : DbContext, IDisposable
        {


            void Commit();
            Task CommitAsync();
            void DetachAll();
            Task<IDbContextTransaction> CreateTransactionAsync();
            Task RollBackTransactionAsync(IDbContextTransaction tran);
            Task CommitTransactionAsync(IDbContextTransaction tran);
            IDbContextTransaction GetContextTransaction();
        }
    
}
