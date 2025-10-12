using Alternance.Domain.Entities;
using MongoDB.Driver;

namespace Alternance.Infrastructure.MongoDb.Collections;

public class JobsRepository : MongoRepository<Job>
{
    private readonly IMongoCollection<Job> _collection;

    public JobsRepository(MongoDbContext context) : base(context)
    {
        _collection = context.GetCollection<Job>("Job");
    }


    // Récupère tous les jobs triés du plus récent au plus ancien
 
    public async Task<List<Job>> GetAllSortedByDateAsync()
    {
        return await _collection
            .Find(_ => true)
            .SortByDescending(j => j.CreatedAt)
            .ToListAsync();
    }


    // Récupère les jobs actifs triés du plus récent au plus ancien
 
    public async Task<List<Job>> GetActiveJobsSortedByDateAsync()
    {
        return await _collection
            .Find(j => j.Status == Domain.Enum.Status.Active)
            .SortByDescending(j => j.CreatedAt)
            .ToListAsync();
    }


    // Récupère les jobs avec pagination, triés du plus récent au plus ancien
 
    public async Task<List<Job>> GetJobsPaginatedAsync(int page, int pageSize)
    {
        return await _collection
            .Find(_ => true)
            .SortByDescending(j => j.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }
}
