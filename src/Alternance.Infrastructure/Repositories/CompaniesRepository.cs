using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;

namespace Alternance.Infrastructure.Repositories;

public class CompaniesRepository : MongoRepository<Company>
{
    public CompaniesRepository(MongoDbContext context) : base(context)
    {
    }
}
