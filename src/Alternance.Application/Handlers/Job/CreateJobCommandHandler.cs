using Alternance.Application.Commands;
using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using Alternance.Domain.Enum;
using MediatR;

namespace Alternance.Application.Handlers.Job;

public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
{
    private readonly IJobRepository _jobRepository;

    public CreateJobCommandHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        //** Parse ContractType enum
        if (!Enum.TryParse<ContractType>(request.ContractType, true, out var contractType))
        {
            throw new ArgumentException($"Invalid contract type: {request.ContractType}");
        }

        //** Create job entity
        Guid jobId = Guid.NewGuid();
        var job = new Domain.Entities.Job(
            jobId,
            request.CompanyId,
            request.Title,
            request.Description,
            request.Location,
            contractType,
            request.Salary
        );

        //** Save to database
        await _jobRepository.AddJobAsync(job);

        return jobId;
    }
}
