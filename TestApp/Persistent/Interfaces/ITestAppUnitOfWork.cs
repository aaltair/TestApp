using TestApp.Core.Entities.Identity;
using TestApp.Core.Entities.Lookup;
using TestApp.Persistent.Contexts;

namespace TestApp.Persistent.Interfaces
{
    public interface ITestAppUnitOfWork : IUnitOfWork<TestAppDbContext>
    {
         IBaseRepository<ApplicationUser> UserRepository { get; }
         IBaseRepository<LookupType> LookupTypeRepository { get; }
         IBaseRepository<LookupItem> LookupItemRepository { get; }
    }
}