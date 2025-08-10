using Microsoft.EntityFrameworkCore;
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

        public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().AnyAsync(predicate);
        }
    }
}
