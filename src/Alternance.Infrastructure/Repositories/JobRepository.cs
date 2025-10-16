using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Alternance.Infrastructure.Repositories;

public class JobRepository : MongoRepository<Job>, IJobRepository
{
    private const string JOB_COLLECTION = "Job";
    private readonly IMongoCollection<Job> _collection;

    public JobRepository(MongoDbContext context) : base(context)
    {
        _collection = context.GetCollection<Job>(JOB_COLLECTION);
    }

    //** Get job by ID
    public async Task<Job?> GetJobByIdAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    //** Get all jobs
    public async Task<List<Job>> GetAllJobsAsync()
    {
        IAsyncCursor<Job> cursor = await _collection.FindAsync(_ => true);
        return await cursor.ToListAsync();
    }

    //** Add job
    public async Task<Job> AddJobAsync(Job job)
    {
        return await AddAsync(job);
    }

    //** Update job
    public async Task UpdateJobAsync(Job job)
    {
        await UpdateAsync(job);
    }

    //** Delete job
    public async Task DeleteJobAsync(Guid id)
    {
        await DeleteAsync(id);
    }
}
