using PetStock.API.CashShifts.Domain.Model.Commands;
using PetStock.API.CashShifts.Interfaces.REST.Resources;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class CloseCashShiftCommandFromResourceAssembler
{
    public static CloseCashShiftCommand ToCommandFromResource(int cashShiftId, CloseCashShiftResource resource) =>
        new(cashShiftId, resource.ClosingAmount);
}