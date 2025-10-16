using Alternance.Application.Commands;
using Alternance.Application.Interfaces;
using Alternance.Domain.Entities;
using Alternance.Domain.Enum;
using MediatR;

namespace Alternance.Application.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Hash the password
        string passwordHash = _passwordHasher.HashPassword(request.Password);

        // Parse UserType enum
        if (!Enum.TryParse<UserType>(request.UserType, true, out var userType))
        {
            throw new ArgumentException($"Invalid user type: {request.UserType}");
        }

        //** Create user entity
        Guid userId = Guid.NewGuid();
        User user = new(
            userId,
            request.Email,
            passwordHash,
            request.FirstName,
            request.LastName,
            userType
        );

        //** Save to database
        await _userRepository.AddAsync(user);

        return userId;
    }
}
