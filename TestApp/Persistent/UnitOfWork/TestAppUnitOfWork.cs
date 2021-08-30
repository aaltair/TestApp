using TestApp.Core.Entities.Identity;
using TestApp.Core.Entities.Lookup;
using TestApp.Persistent.Abstracts;
using TestApp.Persistent.Contexts;
using TestApp.Persistent.Interfaces;

namespace TestApp.Persistent.UnitOfWork
{
    public class TestAppUnitOfWork :  UnitOfWork<TestAppDbContext>, ITestAppUnitOfWork
    {

        public TestAppUnitOfWork(TestAppDbContext context):base(context)
        {
            UserRepository = new BaseRepository<ApplicationUser>(context);
            LookupTypeRepository = new BaseRepository<LookupType>(context);
            LookupItemRepository = new BaseRepository<LookupItem>(context);
        }

        public IBaseRepository<ApplicationUser> UserRepository { get; }
        public IBaseRepository<LookupType> LookupTypeRepository { get; }
        public IBaseRepository<LookupItem> LookupItemRepository { get; }
    }
}