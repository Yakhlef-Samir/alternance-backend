using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;
using MongoDB.Driver;
using ApplicationEntity = Alternance.Domain.Entities.Application;

namespace Alternance.Infrastructure.Repositories;

public class ApplicationRepository : MongoRepository<ApplicationEntity>, IApplicationRepository
{
    private const string APPLICATION_COLLECTION = "Application";
    private readonly IMongoCollection<ApplicationEntity> _collection;

    public ApplicationRepository(MongoDbContext context) : base(context)
    {
        _collection = context.GetCollection<ApplicationEntity>(APPLICATION_COLLECTION);
    }

    //** Get application by ID
    public async Task<ApplicationEntity?> GetApplicationByIdAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    //** Get all applications
    public async Task<List<ApplicationEntity>> GetAllApplicationsAsync()
    {
        IAsyncCursor<ApplicationEntity> cursor = await _collection.FindAsync(_ => true);
        return await cursor.ToListAsync();
    }

    //** Add application
    public async Task<ApplicationEntity> AddApplicationAsync(ApplicationEntity application)
    {
        return await AddAsync(application);
    }

    //** Update application
    public async Task UpdateApplicationAsync(ApplicationEntity application)
    {
        await UpdateAsync(application);
    }

    //** Delete application
    public async Task DeleteApplicationAsync(Guid id)
    {
        await DeleteAsync(id);
    }
}
