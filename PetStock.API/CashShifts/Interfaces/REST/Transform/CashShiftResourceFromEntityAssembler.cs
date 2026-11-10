using PetStock.API.CashShifts.Domain.Model.Aggregates;
using PetStock.API.CashShifts.Interfaces.REST.Resources;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class CashShiftResourceFromEntityAssembler
{
    public static CashShiftResource ToResourceFromEntity(CashShift entity) =>
        new(entity.Id, entity.UserId, entity.OpeningAmount, entity.ClosingAmount, entity.Status, entity.OpenedAt, entity.ClosedAt);
}