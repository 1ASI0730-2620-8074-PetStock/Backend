namespace PetStock.API.IAM.Interfaces.REST.Resources;

public record SessionResource(int Id, int UserId, string Token, DateTimeOffset ExpiresAt);