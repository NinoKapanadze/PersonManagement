using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonManagement.Application.RepoInterfaces.Base;

namespace PersonManagement.Infrastructure.Repositories.Base
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : class
    {
        private protected readonly ILogger<WriteRepository<TEntity>> _logger;
        protected readonly DataContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public WriteRepository(
            ILogger<WriteRepository<TEntity>> logger,
            DataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                if (entity == null)
                {
                    _logger.LogWarning("Attempted to delete a null entity.");
                    return false;
                }

                _dbSet.Remove(entity);
                _logger.LogInformation("Removing model from DB context. Model - {model}", entity.GetType().Name);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing model from DB context. Model - {model}", entity.GetType().Name);
                return false;
            }
        }

        public  TEntity? Add(TEntity model)
        {
            try
            {
                _logger.LogInformation("Adding model to DB context. Model - {model}", model.GetType().Name);
                _dbSet.Add(model);

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding model to DB context. Model - {model}", model.GetType().Name);
                return null;
            }
        }

        public TEntity? Update(TEntity model)
        {
            try
            {
                _dbSet.Update(model);
                _logger.LogInformation("Updating model in DB context. Model - {model}", model.GetType().Name);

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating model in DB context. Model - {model}", model.GetType().Name);

                return null;
            }
        }
    }
}
