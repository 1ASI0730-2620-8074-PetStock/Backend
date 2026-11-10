using PetStock.API.IAM.Application.Services;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Queries;
using PetStock.API.IAM.Domain.Repositories;

namespace PetStock.API.IAM.Application.Internal.QueryServices;

public class SessionQueryService(ISessionRepository sessionRepository) : ISessionQueryService
{
    public async Task<Session?> Handle(GetSessionByIdQuery query, CancellationToken cancellationToken = default) =>
        await sessionRepository.FindByIdAsync(query.Id, cancellationToken);
}