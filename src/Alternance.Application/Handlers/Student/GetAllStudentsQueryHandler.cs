using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers.Student;

public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
{
    private readonly IStudentRepository _studentRepository;

    public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetAllStudentAsync();

        return students.Select(student => new StudentDto(
            student.Id,
            student.UserId,
            student.StudentId,
            student.Phone,
            student.Bio,
            student.ResumeUrl,
            student.Skills,
            student.Location,
            student.ExperienceYears
        )).ToList();
    }
}
