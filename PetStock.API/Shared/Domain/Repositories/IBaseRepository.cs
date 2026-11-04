namespace PetStock.API.Shared.Domain.Repositories;

/// <summary>Basic CRUD operations shared by all repositories.</summary>
/// <typeparam name="TEntity">The entity type</typeparam>
public interface IBaseRepository<TEntity>
{
    /// <summary>Adds a new entity.</summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>Finds an entity by its id, or null if it does not exist.</summary>
    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>Marks an entity as updated.</summary>
    void Update(TEntity entity);

    /// <summary>Removes an entity.</summary>
    void Remove(TEntity entity);

    /// <summary>Lists all entities.</summary>
    Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken = default);
}