using Alternance.Domain.Entities;

namespace Alternance.Infrastructure.MongoDb.Collections;

public class ApplicationsRepository : MongoRepository<Application>
{
    public ApplicationsRepository(MongoDbContext context) : base(context)
    {
    }
}
