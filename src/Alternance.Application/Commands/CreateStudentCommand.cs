using MediatR;

namespace Alternance.Application.Commands;

public record CreateStudentCommand(
    Guid UserId
) : IRequest<Guid>;
