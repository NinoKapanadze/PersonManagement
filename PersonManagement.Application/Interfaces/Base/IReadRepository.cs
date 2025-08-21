using PersonManagement.Shared;
using System.Linq.Expressions;
using System.Threading;

namespace PersonManagement.Application.RepoInterfaces.Base
{
    public interface IReadRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets all entities in the repository.
        /// </summary>
        /// <returns>An IQueryable representing all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets entities based on a predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities.</param>
        /// <returns>An IQueryable representing entities that satisfy the predicate.</returns>
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a single entity based on a predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter entities.</param>
        /// <returns>The first entity that satisfies the predicate, or null if not found.</returns>
        public  Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, 
              string[]? include = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if any entity in the repository satisfies a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to check against entities.</param>
        /// <returns>True if any entity satisfies the predicate; otherwise, false.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        public Task<PagedResult<TEntity>> GetPagedListAsync(int pageIndex,
                                                     int pageSize,
                                                     Expression<Func<TEntity, bool>>? filter = null,
                                                     string[]? includeProperties = null,
                                                     string? orderBy = null,
                                                     bool descending = false,
                                                     CancellationToken cancellationToken = default);
    }
}
