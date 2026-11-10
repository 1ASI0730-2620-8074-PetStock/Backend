using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Interfaces.REST.Resources;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class CashReconciliationResourceFromEntityAssembler
{
    public static CashReconciliationResource ToResourceFromEntity(CashReconciliation entity) =>
        new(entity.Id, entity.CashShiftId, entity.ExpectedAmount, entity.ActualAmount, entity.Difference);
}