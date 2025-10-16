using Alternance.Application.Commands;
using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using MediatR;

namespace Alternance.Application.Handlers.Student;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{
    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        //** Create student entity
        Guid studentId = Guid.NewGuid();
        var student = new Domain.Entities.Student(request.UserId, studentId);

        //** Save to database
        await _studentRepository.AddStudentAsync(student);

        return studentId;
    }
}
