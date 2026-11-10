using PetStock.API.CashShifts.Domain.Model.Commands;

namespace PetStock.API.CashShifts.Domain.Model.Aggregates;

/// <summary>Difference between expected and counted cash of a shift.</summary>
public partial class CashReconciliation
{
    // Constructor for EF Core
    protected CashReconciliation()
    {
    }

    /// <summary>Creates the reconciliation and calculates the difference.</summary>
    public CashReconciliation(CreateCashReconciliationCommand command)
    {
        ArgumentNullException.ThrowIfNull(command);
        CashShiftId = command.CashShiftId;
        ExpectedAmount = command.ExpectedAmount;
        ActualAmount = command.ActualAmount;
        Difference = command.ActualAmount - command.ExpectedAmount;
    }

    public int Id { get; private set; }
    public int CashShiftId { get; private set; }
    public decimal ExpectedAmount { get; private set; }
    public decimal ActualAmount { get; private set; }

    /// <summary>Actual minus expected.</summary>
    public decimal Difference { get; private set; }
}
