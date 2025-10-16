using Alternance.Application.Commands;
using Alternance.Application.Interfaces;
using MediatR;

namespace Alternance.Application.Handlers.Student;

public class UpdateStudentProfileCommandHandler : IRequestHandler<UpdateStudentProfileCommand, bool>
{
    private readonly IStudentRepository _studentRepository;

    public UpdateStudentProfileCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<bool> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
    {
        //** Retrieve student from repository
        var student = await _studentRepository.GetStudentByIdAsync(request.StudentId);

        if (student is null)
            return false;

        //** Update student profile using domain method
        student.UpdateProfile(request.Phone, request.Bio, request.Location, request.ResumeUrl, request.Skills, request.ExperienceYears);

        //** Save changes
        await _studentRepository.UpdateStudentAsync(student);

        return true;
    }
}
