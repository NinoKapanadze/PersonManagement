using Microsoft.EntityFrameworkCore;
using PersonManagement.Application.Common;
using PersonManagement.Application.RepoInterfaces.Base;
using System.Linq.Expressions;

namespace PersonManagement.Infrastructure.Repositories.Base
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public ReadRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
             Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }

        public IQueryable<TEntity> Query()
        {
            return _dbContext.Set<TEntity>().AsQueryable().AsNoTracking();
        }
        public async Task<PagedResult<TEntity>> GetPagedListAsync(int pageIndex,
                                                      int pageSize,
                                                      Expression<Func<TEntity, bool>> filter = null,
                                                      string[] includeProperties = null,
                                                      string orderBy = null,
                                                      bool descending = false,
                                                      CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
                query = query.Where(filter);

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = descending
                    ? query.OrderByDescending(e => EF.Property<object>(e, orderBy))
                    : query.OrderBy(e => EF.Property<object>(e, orderBy));
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip((pageIndex - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync(cancellationToken);

            return new PagedResult<TEntity>(items, totalCount, pageIndex, pageSize);
        }
    }
}
