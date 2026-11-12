namespace PetStock.API.Sales.Domain.Model.Commands;

/// <summary>Data needed to register a sale. CustomerId is null for anonymous sales.</summary>
public record CreateSaleCommand(
    int UserId,
    int? CustomerId,
    string PaymentMethod,
    IEnumerable<CreateSaleItemCommand> Items);