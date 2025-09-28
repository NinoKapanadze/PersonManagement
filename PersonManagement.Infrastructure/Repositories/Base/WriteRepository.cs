using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonManagement.Application.RepoInterfaces.Base;
using PersonManagement.Domain;

namespace PersonManagement.Infrastructure.Repositories.Base
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity<int>
    {
        protected readonly DataContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;
        protected readonly ILogger<WriteRepository<TEntity>> _logger;

        public WriteRepository(
            DataContext dbContext, ILogger<WriteRepository<TEntity>> logger)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    return false;
                }

                entity.MarkAsDeleted();
                _dbSet.Update(entity);

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return false;
            }
        }

        public bool Add(TEntity model)
        {
            try
            {
                _dbSet.Add(model);

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return false;
            }
        }

        public bool Update(TEntity model)
        {
            try
            {
                _dbSet.Update(model);

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message, ex);
                return false;
            }
        }
    }
}
