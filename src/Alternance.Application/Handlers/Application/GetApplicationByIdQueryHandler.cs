using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers.Application;

public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, ApplicationDto?>
{
    private readonly IApplicationRepository _applicationRepository;

    public GetApplicationByIdQueryHandler(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<ApplicationDto?> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        var application = await _applicationRepository.GetApplicationByIdAsync(request.ApplicationId);
        if (application is null)
            return null;

        return new ApplicationDto(
            application.Id,
            application.StudentId,
            application.JobId,
            application.Status.ToString(),
            application.CoverLetter,
            application.ResumeUrl,
            application.AppliedAt
        );
    }
}
