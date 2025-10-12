using Alternance.Domain.Entities;

namespace Alternance.Infrastructure.MongoDb.Collections;

public class StudentsRepository : MongoRepository<Student>
{
    public StudentsRepository(MongoDbContext context) : base(context)
    {
    }
}
