using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Customers.Interfaces.REST.Resources;

/// <summary>Body of POST /customers. Only the name is required.</summary>
public record CreateCustomerResource(
    [Required(ErrorMessage = "RequiredCustomerName")]
    [StringLength(150, ErrorMessage = "CustomerNameTooLong")]
    string Name,

    [StringLength(150, ErrorMessage = "CustomerEmailTooLong")]
    string? Email,

    [StringLength(30, ErrorMessage = "CustomerPhoneTooLong")]
    string? Phone,

    [StringLength(255, ErrorMessage = "CustomerAddressTooLong")]
    string? Address);