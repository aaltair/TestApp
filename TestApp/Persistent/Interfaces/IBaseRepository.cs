using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Core.Dtos;

namespace TestApp.Persistent.Interfaces
{
    public interface IBaseRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> All();

        Task<IEnumerable<TEntity>> GetByIncludeEntity(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        Task<IEnumerable<TEntity>> GetByIncludeEntity(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<PagedResult<TEntity>> GetByIncludePagedResult(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        Task<PagedResult<TEntity>> GetByIncludePagedResult(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> GetAllEntity(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? page = null,
            int? pageSize = null);
        Task<PagedResult<TEntity>> GetAllPageResult(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? page = null,
            int? pageSize = null);

        Task<TEntity> GetById(int id,
            bool withTrack = true,
            params Expression<Func<TEntity, object>>[] includeProperties);

        public Task<TEntity> GetById(object id);
        public Task<int> Count(Expression<Func<TEntity, bool>> predicate = null);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        Task Delete(object id);


        Task<TEntity> GetSingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool ignoreQueryFilter = false,
            bool asNoTracking = false);

    }

}
