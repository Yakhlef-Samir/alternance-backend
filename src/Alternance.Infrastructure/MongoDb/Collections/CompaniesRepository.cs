using Alternance.Domain.Entities;

namespace Alternance.Infrastructure.MongoDb.Collections;

public class CompaniesRepository : MongoRepository<Company>
{
    public CompaniesRepository(MongoDbContext context) : base(context)
    {
    }
}
