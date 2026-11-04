namespace PetStock.API.IAM.Domain.Model.Commands;

/// <summary>Data needed to create a user account.</summary>
public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string Phone,
    string Role);