namespace PetStock.API.IAM.Interfaces.REST.Resources;

public record UserResource(int Id, string FirstName, string LastName, string Email, string Phone, string Role);