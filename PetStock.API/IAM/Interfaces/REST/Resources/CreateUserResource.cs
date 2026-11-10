using System.ComponentModel.DataAnnotations;

namespace PetStock.API.IAM.Interfaces.REST.Resources;

public record CreateUserResource(
    [Required(ErrorMessage = "UserFirstNameRequired")]
    [StringLength(100, ErrorMessage = "UserFirstNameTooLong")]
    string FirstName,
    [Required(ErrorMessage = "UserLastNameRequired")]
    [StringLength(100, ErrorMessage = "UserLastNameTooLong")]
    string LastName,
    [Required(ErrorMessage = "UserEmailRequired")]
    [EmailAddress(ErrorMessage = "UserEmailInvalid")]
    [StringLength(200, ErrorMessage = "UserEmailTooLong")]
    string Email,
    [Required(ErrorMessage = "UserPasswordRequired")]
    [MinLength(6, ErrorMessage = "UserPasswordTooShort")]
    string Password,
    [StringLength(20, ErrorMessage = "UserPhoneTooLong")]
    string? Phone,
    [StringLength(40, ErrorMessage = "UserRoleTooLong")]
    string? Role);