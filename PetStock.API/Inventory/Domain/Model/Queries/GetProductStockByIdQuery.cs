namespace PetStock.API.Inventory.Domain.Model.Queries;

/// <summary>Query to get one product stock by its id.</summary>
public record GetProductStockByIdQuery(int Id);