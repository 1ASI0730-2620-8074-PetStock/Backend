namespace PetStock.API.Purchasing.Domain.Model.Queries;

/// <summary>Query to get one purchase order by its id.</summary>
public record GetPurchaseOrderByIdQuery(int Id);