using Alternance.Domain.Entities;

namespace Alternance.Application.Interfaces;

public interface IJobRepository
{
    Task<Job?> GetJobByIdAsync(Guid id);
    Task<List<Job>> GetAllJobsAsync();
    Task<Job> AddJobAsync(Job job);
    Task UpdateJobAsync(Job job);
    Task DeleteJobAsync(Guid id);
}
