namespace PersonManagement.Application.RepoInterfaces.Base
{
    public interface IWriteRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="model">The entity to add.</param>
        /// <returns>The added entity with its persistence-generated fields populated, if the operation is successful; otherwise, null.</returns>
         bool Add(TEntity model);

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="model">The entity to update with updated values.</param>
        /// <returns>The updated entity, if the operation is successful; otherwise, null.</returns>
        bool Update(TEntity model);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>true if the operation is successful; otherwise, false.</returns>
        bool Delete(TEntity entity);
    }
}
