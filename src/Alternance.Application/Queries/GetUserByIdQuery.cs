using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetUserByIdQuery(Guid UserId) : IRequest<UserDto?>;
