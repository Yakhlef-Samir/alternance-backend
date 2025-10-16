using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using Alternance.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Alternance.Infrastructure.Repositories;

public class CompanyRepository : MongoRepository<Company>, ICompanyRepository
{
    private const string COMPANY_COLLECTION = "Company";
    private readonly IMongoCollection<Company> _collection;

    public CompanyRepository(MongoDbContext context) : base(context)
    {
        _collection = context.GetCollection<Company>(COMPANY_COLLECTION);
    }

    //** Get company by ID
    public async Task<Company?> GetCompanyByIdAsync(Guid id)
    {
        return await GetByIdAsync(id);
    }

    //** Get all companies
    public async Task<List<Company>> GetAllCompaniesAsync()
    {
        IAsyncCursor<Company> cursor = await _collection.FindAsync(_ => true);
        return await cursor.ToListAsync();
    }

    //** Add company
    public async Task<Company> AddCompanyAsync(Company company)
    {
        return await AddAsync(company);
    }

    //** Update company
    public async Task UpdateCompanyAsync(Company company)
    {
        await UpdateAsync(company);
    }

    //** Delete company
    public async Task DeleteCompanyAsync(Guid id)
    {
        await DeleteAsync(id);
    }
}
