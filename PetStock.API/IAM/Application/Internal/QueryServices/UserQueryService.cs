using PetStock.API.IAM.Application.Services;
using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Queries;
using PetStock.API.IAM.Domain.Repositories;

namespace PetStock.API.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default) =>
        await userRepository.ListAsync(cancellationToken);

    public async Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default) =>
        await userRepository.FindByIdAsync(query.Id, cancellationToken);
}