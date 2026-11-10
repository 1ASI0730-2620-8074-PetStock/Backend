namespace PetStock.API.IAM.Domain.Model.Commands;

public record UpdateUserCommand(int UserId, string FirstName, string LastName, string Email, string Phone);