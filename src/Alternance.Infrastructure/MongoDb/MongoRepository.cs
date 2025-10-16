using System.Linq.Expressions;
using System.Reflection;
using Alternance.Infrastructure.Interface;
using MongoDB.Driver;

namespace Alternance.Infrastructure.MongoDb;

public class MongoRepository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;
    private const string FIELD_ID = "Id";

    public MongoRepository(MongoDbContext context)
    {
        _collection = context.GetCollection<T>(typeof(T).Name);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq(FIELD_ID, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _collection.Find(predicate).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
       PropertyInfo? idProperty = typeof(T).GetProperty(FIELD_ID);
        if (idProperty is not null)
        {
            object? id = idProperty.GetValue(entity);
             FilterDefinition<T>? filter = Builders<T>.Filter.Eq(FIELD_ID, id);
            await _collection.ReplaceOneAsync(filter, entity);
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        FilterDefinition<T>? filter = Builders<T>.Filter.Eq(FIELD_ID, id);
        await _collection.DeleteOneAsync(filter);
    }
}
