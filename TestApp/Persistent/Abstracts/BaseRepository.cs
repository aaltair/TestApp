using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestApp.Core.Dtos;
using TestApp.Extentions;
using TestApp.Persistent.Interfaces;

namespace TestApp.Persistent.Abstracts
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        internal DbSet<TEntity> DbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;

            DbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> All()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByIncludeEntity(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            var results = DbSet.AsQueryable();
            if (includes != null)
                results = includes(results);

            if (predicate != null)
                results = results.Where(predicate);

            if (orderBy != null)
            {
                results = orderBy(results);
            }

            if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                results = results.ApplyPaging(page.Value, pageSize.Value);

            // orderBy?.Invoke(results);
            return await results.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByIncludeEntity(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> results = GetAllIncluding(withTrack: false, includeProperties);

            if (predicate != null)
                results = results.Where(predicate);

            if (orderBy != null)
            {
                results = orderBy(results);
            }

            if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                results = results.ApplyPaging(page.Value, pageSize.Value);

            // orderBy?.Invoke(results);
            return await results.ToListAsync();
        }

        public async Task<PagedResult<TEntity>> GetByIncludePagedResult(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null)
        {
            var results = DbSet.AsQueryable();
            if (includes != null)
                results = includes(results);

            int count = 0;
            if (predicate != null)
                results = results.Where(predicate);
            if (orderBy != null)
            {
                results = orderBy(results);
            }
            if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
            {
                count = await results.CountAsync();
                results = results.ApplyPaging(page.GetValueOrDefault(), pageSize.GetValueOrDefault());
            }
            //orderBy?.Invoke(results);
            return new PagedResult<TEntity>()
            {
                Result = await results.ToListAsync(),
                TotalCount = count
            };
        }

        public async Task<PagedResult<TEntity>> GetByIncludePagedResult(Expression<Func<TEntity, bool>> predicate = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> results = GetAllIncluding(withTrack: false, includeProperties);

            int count = 0;

            if (predicate != null)
                results = results.Where(predicate);

            if (orderBy != null)
            {
                results = orderBy(results);
            }

            if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
            {
                count = await results.CountAsync();
                results = results.ApplyPaging(page.GetValueOrDefault(), pageSize.GetValueOrDefault());

            }


            //orderBy?.Invoke(results);

            return new PagedResult<TEntity>()
            {
                Result = await results.ToListAsync(),
                TotalCount = count
            };
        }

        private IQueryable<TEntity> GetAllIncluding
            (bool withTrack, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = null;

            if (withTrack)
                queryable = DbSet.AsQueryable();
            else
                queryable = DbSet.AsNoTracking();

            return includeProperties.Aggregate
                (queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task<IEnumerable<TEntity>> GetAllEntity(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> results = DbSet.AsNoTracking();

            if (predicate != null)
                results = results.Where(predicate);

            if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
                results = results.ApplyPaging(page.Value, pageSize.Value);

            if (orderBy != null)
            {
                results = orderBy?.Invoke(results);
            }

            return await results.ToListAsync();
        }


        public async Task<TEntity> GetSingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool ignoreQueryFilter = false,
            bool asNoTracking = false)
        {
            var query = DbSet.AsQueryable();
            if (includes != null)
                query = includes(query);

            if (predicate != null)
                query = query.Where(predicate);

            if (asNoTracking)
                query = query.AsNoTracking();

            if (ignoreQueryFilter)
                query.IgnoreQueryFilters();

            return await query.SingleOrDefaultAsync();
        }

        public async Task<PagedResult<TEntity>> GetAllPageResult(Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> results = DbSet.AsNoTracking();

            if (predicate != null)
                results = results.Where(predicate);

            int count = 0;

            if (page.GetValueOrDefault() > 0 && pageSize.GetValueOrDefault() > 0)
            {
                count = await results.CountAsync();
                results = results.ApplyPaging(page.GetValueOrDefault(), pageSize.GetValueOrDefault());
            }

            orderBy?.Invoke(results);

            return new PagedResult<TEntity>()
            {
                Result = await results.ToListAsync(),
                TotalCount = count
            };
        }

        public async Task<TEntity> GetById(int id,
            bool withTrack = true,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = GetAllIncluding(withTrack, includeProperties);

            Expression<Func<TEntity, bool>> lambda = QueryableExtensions.BuildLambdaForFindByKey<TEntity>(id);

            if (withTrack)
                return await query.SingleOrDefaultAsync(lambda);

            return await query.AsNoTracking().SingleOrDefaultAsync(lambda);
        }


        public async Task<TEntity> GetById(object id)
        {
            DbSet.AsNoTracking();

            var entryToFind = await DbSet.FindAsync(id);

            if (entryToFind != null)
                _context.Entry(entryToFind).State = EntityState.Detached;

            return entryToFind;
        }

        public async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            IQueryable<TEntity> results = DbSet.AsNoTracking();
            if (predicate != null)
                return await results.CountAsync(predicate);
            return await results.CountAsync();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(object id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity);
        }
    }
}
