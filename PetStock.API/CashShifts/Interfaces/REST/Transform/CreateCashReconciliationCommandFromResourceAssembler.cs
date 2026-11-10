using PetStock.API.CashShifts.Domain.Model.Commands;
using PetStock.API.CashShifts.Interfaces.REST.Resources;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class CreateCashReconciliationCommandFromResourceAssembler
{
    public static CreateCashReconciliationCommand ToCommandFromResource(CreateCashReconciliationResource resource) =>
        new(resource.CashShiftId, resource.ExpectedAmount, resource.ActualAmount);
}