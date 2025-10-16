using Alternance.Infrastructure.MongoDb;
using ApplicationEntity = Alternance.Domain.Entities.Application;

namespace Alternance.Infrastructure.Repositories;

public class ApplicationsRepository : MongoRepository<ApplicationEntity>
{
    public ApplicationsRepository(MongoDbContext context) : base(context)
    {
    }
}
