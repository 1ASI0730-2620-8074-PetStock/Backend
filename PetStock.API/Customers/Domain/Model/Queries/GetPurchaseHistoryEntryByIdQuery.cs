namespace PetStock.API.Customers.Domain.Model.Queries;

/// <summary>Query to get one purchase history entry by its id.</summary>
public record GetPurchaseHistoryEntryByIdQuery(int Id);