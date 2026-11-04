namespace PetStock.API.Shared.Domain.Repositories;

/// <summary>Saves all pending changes in a single transaction.</summary>
public interface IUnitOfWork
{
    /// <summary>Commits the pending changes to the database.</summary>
    Task CompleteAsync(CancellationToken cancellationToken = default);
}