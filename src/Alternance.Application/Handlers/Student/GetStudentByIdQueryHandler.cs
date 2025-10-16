using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers.Student
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentByIdQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetStudentByIdAsync(request.Id);
            if (student is null)
            {
                return null;
            }

            return new StudentDto(
                student.Id,
                student.UserId,
                student.StudentId,
                student.Phone,
                student.Bio,
                student.ResumeUrl,
                student.Skills,
                student.Location,
                student.ExperienceYears
            );
        }
    }
}