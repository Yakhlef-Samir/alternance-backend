using Alternance.Domain.Common;
using Alternance.Domain.Enum;

namespace Alternance.Domain.Entities;

public class User : BaseEntity
{
    public Guid UserId { get; private set; } = Guid.NewGuid();
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public UserType UserType { get; private set; }  // "Student" or "Company"

    private User() { } // For ORM

    public User(Guid userId, string email, string passwordHash, string firstName, string lastName, UserType userType)
    {
        UserId = userId;
        Email = email;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        UserType = userType;
    }

    public void UpdateProfile(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
