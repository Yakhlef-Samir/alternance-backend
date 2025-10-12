using Alternance.Domain.Entities;

namespace Alternance.Infrastructure.MongoDb.Collections;

public class UsersRepository : MongoRepository<User>
{
    public UsersRepository(MongoDbContext context) : base(context)
    {
    }
}
