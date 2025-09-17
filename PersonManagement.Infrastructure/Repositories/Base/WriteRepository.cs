using Microsoft.EntityFrameworkCore;
using PersonManagement.Application.RepoInterfaces.Base;
using PersonManagement.Domain;

namespace PersonManagement.Infrastructure.Repositories.Base
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity<int>
    {
        protected readonly DataContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public WriteRepository(
            DataContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
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
            catch (Exception)
            {
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
            catch (Exception)
            {// TODO: Log the exception 
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
            catch (Exception)
            {
                return false;
            }
        }
    }
}
