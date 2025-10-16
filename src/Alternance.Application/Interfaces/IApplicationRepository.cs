using Alternance.Domain.Entities;

namespace Alternance.Application.Interfaces;

public interface IApplicationRepository
{
    Task<Alternance.Domain.Entities.Application?> GetApplicationByIdAsync(Guid id);
    Task<List<Alternance.Domain.Entities.Application>> GetAllApplicationsAsync();
    Task<Alternance.Domain.Entities.Application> AddApplicationAsync(Alternance.Domain.Entities.Application application);
    Task UpdateApplicationAsync(Alternance.Domain.Entities.Application application);
    Task DeleteApplicationAsync(Guid id);
}
