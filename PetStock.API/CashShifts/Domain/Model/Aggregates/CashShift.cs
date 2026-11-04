using PetStock.API.CashShifts.Domain.Model.Commands;

namespace PetStock.API.CashShifts.Domain.Model.Aggregates;

/// <summary>Cash register shift, from opening to closing.</summary>
public partial class CashShift
{
    // Constructor for EF Core
    protected CashShift()
    {
        Status = string.Empty;
    }

    /// <summary>Opens a shift from the command.</summary>
    public CashShift(CreateCashShiftCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        UserId = command.UserId;
        OpeningAmount = command.OpeningAmount;
        OpenedAt = DateTimeOffset.UtcNow;
        Status = "Open";
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public DateTimeOffset OpenedAt { get; private set; }

    /// <summary>Null while the shift is open.</summary>
    public DateTimeOffset? ClosedAt { get; private set; }

    public decimal OpeningAmount { get; private set; }

    /// <summary>Null while the shift is open.</summary>
    public decimal? ClosingAmount { get; private set; }

    /// <summary>Open or Closed.</summary>
    public string Status { get; private set; }

    /// <summary>Closes the shift with the counted cash.</summary>
    public void Close(decimal closingAmount)
    {
        ClosingAmount = closingAmount;
        ClosedAt = DateTimeOffset.UtcNow;
        Status = "Closed";
    }
}
