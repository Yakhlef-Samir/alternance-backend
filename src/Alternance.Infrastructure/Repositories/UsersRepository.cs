using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;

namespace Alternance.Infrastructure.Repositories;

public class UsersRepository : MongoRepository<User>
{
    public UsersRepository(MongoDbContext context) : base(context)
    {
    }
}
