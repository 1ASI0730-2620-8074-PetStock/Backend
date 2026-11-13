using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Customers.Interfaces.REST.Resources;

/// <summary>Body of POST /purchase-history-entries.</summary>
public record CreatePurchaseHistoryEntryResource(
    [Range(1, int.MaxValue, ErrorMessage = "HistoryCustomerIdInvalid")]
    int CustomerId,

    [Range(1, int.MaxValue, ErrorMessage = "HistorySaleIdInvalid")]
    int SaleId,

    [Range(0.0, 999999.99, ErrorMessage = "HistoryTotalInvalid")]
    decimal Total);