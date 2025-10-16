using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        if (user == null)
            return null;

        return new UserDto(
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.UserType.ToString(),
            user.CreatedAt
        );
    }
}
