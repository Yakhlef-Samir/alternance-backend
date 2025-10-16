using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries
{
    public record GetStudentByIdQuery(Guid id) : IRequest<StudentDto?>
    {
        public Guid Id { get; } = id;
    }
}