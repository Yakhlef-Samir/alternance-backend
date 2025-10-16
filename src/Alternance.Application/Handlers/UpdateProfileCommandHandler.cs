using Alternance.Application.Commands;
using Alternance.Application.Interfaces;
using MediatR;

namespace Alternance.Application.Handlers;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateProfileCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        // Retrieve user from repository
        var user = await _userRepository.GetByIdAsync(request.UserId);
        
        if (user is null)
            return false;

        // Update user profile using domain method
        if (request.FirstName is not null && request.LastName is not null && request.Email is not null)
        {
            user.UpdateProfile(request.FirstName, request.LastName, request.Email);
        }

        // Save changes
        await _userRepository.UpdateAsync(user);
        
        return true;
    }
}
