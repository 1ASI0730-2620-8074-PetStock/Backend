using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Sales.Interfaces.REST.Resources;

/// <summary>Body of POST /sales. CustomerId is optional.</summary>
public record CreateSaleResource(
    [Range(1, int.MaxValue, ErrorMessage = "SaleUserIdInvalid")]
    int UserId,

    int? CustomerId,

    [Required(ErrorMessage = "RequiredSalePaymentMethod")]
    [StringLength(40, ErrorMessage = "SalePaymentMethodTooLong")]
    string PaymentMethod,

    [Required(ErrorMessage = "RequiredSaleItems")]
    [MinLength(1, ErrorMessage = "RequiredSaleItems")]
    List<CreateSaleItemResource> Items);