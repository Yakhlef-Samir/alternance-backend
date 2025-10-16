using Alternance.Application.Interfaces;
using Alternance.Infrastructure.Cache;
using Alternance.Infrastructure.Interface;
using Alternance.Infrastructure.MongoDb;
using Alternance.Infrastructure.Repositories;
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
        services.AddScoped<JobRepository>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<ApplicationRepository>();
        services.AddScoped<IApplicationRepository, ApplicationRepository>();
        services.AddScoped<CompanyRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<StudentRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<UsersRepository>();
        services.AddScoped<IUserRepository, UsersRepository>();

        // Legacy support - keep generic repository for backward compatibility
        services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

        // Redis Cache
        services.AddSingleton<IRedisCache, RedisCache>();

        // External Services
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IAIService, AIService>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
