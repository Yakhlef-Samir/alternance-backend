using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers.Job;

public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDto?>
{
    private readonly IJobRepository _jobRepository;

    public GetJobByIdQueryHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<JobDto?> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
    {
        var job = await _jobRepository.GetJobByIdAsync(request.JobId);
        if (job is null)
            return null;

        return new JobDto(
            job.Id,
            job.CompanyId,
            job.Title,
            job.Description,
            job.Location,
            job.ContractType.ToString(),
            job.Salary,
            job.CreatedAt
        );
    }
}
