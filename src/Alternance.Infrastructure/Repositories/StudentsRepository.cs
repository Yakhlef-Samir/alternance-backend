using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;

namespace Alternance.Infrastructure.Repositories;

public class StudentsRepository : MongoRepository<Student>
{
    public StudentsRepository(MongoDbContext context) : base(context)
    {
    }
}
