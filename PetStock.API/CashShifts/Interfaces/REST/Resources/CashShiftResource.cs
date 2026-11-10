namespace PetStock.API.CashShifts.Interfaces.REST.Resources;

public record CashShiftResource(int Id, int UserId, decimal OpeningAmount, decimal? ClosingAmount, string Status, DateTimeOffset OpenedAt, DateTimeOffset? ClosedAt);