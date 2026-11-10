using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Queries;

namespace PetStock.API.IAM.Application.Services;

public interface ISessionQueryService
{
    Task<Session?> Handle(GetSessionByIdQuery query, CancellationToken cancellationToken = default);
}