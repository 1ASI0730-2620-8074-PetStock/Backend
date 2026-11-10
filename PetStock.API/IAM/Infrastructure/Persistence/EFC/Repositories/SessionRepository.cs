using PetStock.API.IAM.Domain.Model.Aggregates;
using PetStock.API.IAM.Domain.Repositories;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using PetStock.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace PetStock.API.IAM.Infrastructure.Persistence.EFC.Repositories;

public class SessionRepository(AppDbContext context) : BaseRepository<Session>(context), ISessionRepository
{
}
