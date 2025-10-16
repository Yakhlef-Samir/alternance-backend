using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers.Job;

public class GetAllJobsQueryHandler : IRequestHandler<GetAllJobsQuery, List<JobDto>>
{
    private readonly IJobRepository _jobRepository;

    public GetAllJobsQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<List<JobDto>> Handle(GetAllJobsQuery request, CancellationToken cancellationToken)
    {
        var jobs = await _jobRepository.GetAllJobsAsync();

        return jobs.Select(job => new JobDto(
            job.Id,
            job.CompanyId,
            job.Title,
            job.Description,
            job.Location,
            job.ContractType.ToString(),
            job.Salary,
            job.CreatedAt
        )).ToList();
    }
}
