namespace BaseTemplate.Domain.Interfaces;

/// <summary>
/// Base repository interface for domain-driven design.
/// Implement this interface in Infrastructure layer for specific entities.
/// </summary>
/// <typeparam name="T">The entity type</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>Gets an entity by its identifier.</summary>
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>Gets all entities.</summary>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Adds a new entity to the repository.</summary>
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>Removes an entity from the repository.</summary>
    Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
}
