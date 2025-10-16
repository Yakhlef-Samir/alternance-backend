using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using Alternance.Domain.Enum;
using Alternance.Infrastructure.MongoDb;
using MongoDB.Driver;

namespace Alternance.Infrastructure.Repositories;

public class UsersRepository : MongoRepository<User>, IUserRepository
{
    private const string USER_COLLECTION = "User";
    private readonly IMongoCollection<User> _collection;

    public UsersRepository(MongoDbContext context) : base(context)
    {
        _collection = context.GetCollection<User>(USER_COLLECTION);
    }


    //** Get user by email
    public async Task<User?> GetByEmailAsync(string email)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq(u => u.Email, email);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }


    //** Get all users
    public new async Task<List<User>> GetAllAsync()
    {
        IAsyncCursor<User> cursor = await _collection.FindAsync(User => true);
        return await cursor.ToListAsync();
    }

}
