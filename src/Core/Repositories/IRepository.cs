namespace HexaContent.Core.Repositories;

/// <summary>
/// Interface for a generic repository that provides basic CRUD operations.
/// </summary>
/// <typeparam name="TModel">The type of the model.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
public interface IRepository<TModel, TKey>
{
    /// <summary>
    /// Finds an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity if found; otherwise, null.</returns>
    Task<TModel?> FindAsync(TKey id);

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    Task DeleteAsync(TKey id);

    /// <summary>
    /// Adds a new entity to the repository.
    /// </summary>
    /// <param name="model">The entity to add.</param>
    Task AddAsync(TModel model);

    /// <summary>
    /// Counts the total number of entities in the repository.
    /// </summary>
    /// <returns>The total number of entities.</returns>
    Task<int> CountAsync();

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    Task<List<TModel>> GetAllAsync();
}
