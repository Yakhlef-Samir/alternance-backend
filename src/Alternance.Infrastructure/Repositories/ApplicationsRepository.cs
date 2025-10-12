using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;

namespace Alternance.Infrastructure.Repositories;

public class ApplicationsRepository : MongoRepository<Application>
{
    public ApplicationsRepository(MongoDbContext context) : base(context)
    {
    }
}
