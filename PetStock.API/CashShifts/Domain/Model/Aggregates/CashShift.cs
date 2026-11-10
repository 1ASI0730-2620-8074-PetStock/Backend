using PetStock.API.CashShifts.Domain.Model.Commands;

namespace PetStock.API.CashShifts.Domain.Model.Aggregates;

public partial class CashShift
{
    protected CashShift()
    {
        Status = string.Empty;
    }

    public CashShift(CreateCashShiftCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        if (command.OpeningAmount < 0)
            throw new ArgumentException("Opening amount cannot be negative.", nameof(command));

        UserId = command.UserId;
        OpeningAmount = command.OpeningAmount;
        OpenedAt = DateTimeOffset.UtcNow;
        Status = "Open";
    }

    public int Id { get; private set; }
    public int UserId { get; private set; }
    public DateTimeOffset OpenedAt { get; private set; }
    public DateTimeOffset? ClosedAt { get; private set; }
    public decimal OpeningAmount { get; private set; }
    public decimal? ClosingAmount { get; private set; }
    public string Status { get; private set; }

    public void Close(decimal closingAmount)
    {
        if (Status == "Closed")
            throw new InvalidOperationException("The cash shift is already closed.");
        if (closingAmount < 0)
            throw new ArgumentException("Closing amount cannot be negative.", nameof(closingAmount));

        ClosingAmount = closingAmount;
        ClosedAt = DateTimeOffset.UtcNow;
        Status = "Closed";
    }
}
