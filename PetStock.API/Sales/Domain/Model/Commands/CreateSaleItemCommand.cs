namespace PetStock.API.Sales.Domain.Model.Commands;

/// <summary>One product line of a new sale.</summary>
public record CreateSaleItemCommand(
    int ProductId,
    int Quantity,
    decimal UnitPrice);