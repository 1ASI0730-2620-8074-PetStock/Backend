using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Model.Queries;

namespace PetStock.API.IAM.Application.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default);
    Task<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default);
}