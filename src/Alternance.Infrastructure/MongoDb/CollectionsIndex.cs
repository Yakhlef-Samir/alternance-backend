using Alternance.Infrastructure.MongoDb.Collections;

namespace Alternance.Infrastructure.MongoDb;

public class CollectionsIndex
{
    public JobsRepository Jobs { get; }
    public ApplicationsRepository Applications { get; }
    public CompaniesRepository Companies { get; }
    public StudentsRepository Students { get; }
    public UsersRepository Users { get; }

    public CollectionsIndex(MongoDbContext context)
    {
        Jobs = new JobsRepository(context);
        Applications = new ApplicationsRepository(context);
        Companies = new CompaniesRepository(context);
        Students = new StudentsRepository(context);
        Users = new UsersRepository(context);
    }
}
