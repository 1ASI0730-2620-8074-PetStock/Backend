using PetStock.API.CashShifts.Domain.Model.Commands;
using PetStock.API.CashShifts.Interfaces.REST.Resources;

namespace PetStock.API.CashShifts.Interfaces.REST.Transform;

public static class CreateCashShiftCommandFromResourceAssembler
{
    public static CreateCashShiftCommand ToCommandFromResource(CreateCashShiftResource resource) =>
        new(resource.UserId, resource.OpeningAmount);
}