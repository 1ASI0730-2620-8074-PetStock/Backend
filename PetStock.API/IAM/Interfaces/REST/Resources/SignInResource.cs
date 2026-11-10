using System.ComponentModel.DataAnnotations;

namespace PetStock.API.IAM.Interfaces.REST.Resources;

public record SignInResource(
    [Required(ErrorMessage = "SessionEmailRequired")]
    string Email,
    [Required(ErrorMessage = "SessionPasswordRequired")]
    string Password);