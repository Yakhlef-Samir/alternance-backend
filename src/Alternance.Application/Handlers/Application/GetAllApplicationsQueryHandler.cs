using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers.Application;

public class GetAllApplicationsQueryHandler : IRequestHandler<GetAllApplicationsQuery, List<ApplicationDto>>
{
    private readonly IApplicationRepository _applicationRepository;

    public GetAllApplicationsQueryHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<List<ApplicationDto>> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = await _applicationRepository.GetAllApplicationsAsync();

        return applications.Select(application => new ApplicationDto(
            application.Id,
            application.StudentId,
            application.JobId,
            application.Status.ToString(),
            application.CoverLetter,
            application.ResumeUrl,
            application.AppliedAt
        )).ToList();
    }
}
