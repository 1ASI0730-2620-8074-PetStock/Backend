using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Sales.Interfaces.REST.Resources;

/// <summary>Body of POST /payments.</summary>
public record CreatePaymentResource(
    [Range(1, int.MaxValue, ErrorMessage = "PaymentSaleIdInvalid")]
    int SaleId,

    [Range(0.01, 999999.99, ErrorMessage = "PaymentAmountInvalid")]
    decimal Amount,

    [Required(ErrorMessage = "RequiredSalePaymentMethod")]
    [StringLength(40, ErrorMessage = "SalePaymentMethodTooLong")]
    string PaymentMethod);