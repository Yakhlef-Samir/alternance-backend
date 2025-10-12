using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text.Json;

namespace Alternance.Infrastructure.Cache;

public class RedisCache : IRedisCache
{
    private readonly IDatabase _database;

    public RedisCache(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Redis");
        var connection = ConnectionMultiplexer.Connect(connectionString ?? "localhost:6379");
        _database = connection.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);
        
        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var serialized = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, serialized, expiration);
    }

    public async Task RemoveAsync(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        return await _database.KeyExistsAsync(key);
    }
}
