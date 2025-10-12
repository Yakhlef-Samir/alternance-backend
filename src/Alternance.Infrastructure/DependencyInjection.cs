using Alternance.Infrastructure.Cache;
using Alternance.Infrastructure.Interface;
using Alternance.Infrastructure.MongoDb;
using Alternance.Infrastructure.MongoDb.Collections;
using Alternance.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alternance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // MongoDB
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<CollectionsIndex>();
        services.AddScoped<JobsRepository>();
        services.AddScoped<ApplicationsRepository>();
        services.AddScoped<CompaniesRepository>();
        services.AddScoped<StudentsRepository>();
        services.AddScoped<UsersRepository>();

        // Legacy support - keep generic repository for backward compatibility
        services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

        // Redis Cache
        services.AddSingleton<IRedisCache, RedisCache>();

        // External Services
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IAIService, AIService>();

        return services;
    }
}
