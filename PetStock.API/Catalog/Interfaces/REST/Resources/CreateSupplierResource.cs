using System.ComponentModel.DataAnnotations;

namespace PetStock.API.Catalog.Interfaces.REST.Resources;

/// <summary>Body of POST /suppliers. Only the company name is required.</summary>
public record CreateSupplierResource(
    [Required(ErrorMessage = "RequiredSupplierCompanyName")]
    [StringLength(150, ErrorMessage = "SupplierCompanyNameTooLong")]
    string CompanyName,
    [StringLength(100, ErrorMessage = "SupplierFieldTooLong")]
    string? ContactName,
    [StringLength(20, ErrorMessage = "SupplierFieldTooLong")]
    string? Phone,
    [StringLength(150, ErrorMessage = "SupplierFieldTooLong")]
    string? Email);