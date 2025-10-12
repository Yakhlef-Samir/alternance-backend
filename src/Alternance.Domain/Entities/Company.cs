using Alternance.Domain.Common;

namespace Alternance.Domain.Entities;

public class Company : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid CompanyId { get; private set; }
    public string CompanyName { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string? Industry { get; private set; }
    public string? Website { get; private set; }
    public string? Location { get; private set; }
    public int? EmployeeCount { get; private set; }

    private Company() { } // For ORM

    public Company(Guid companyId, Guid userId, string companyName)
    {
        CompanyId = companyId;
        UserId = userId;
        CompanyName = companyName;
    }

    public void UpdateProfile(string? companyName,string? description, string? industry, string? website, string? location, int? employeeCount)
    {
        if (companyName is not null) CompanyName = companyName;
        if (description is not null) Description = description;
        if (industry is not null) Industry = industry;
        if (website is not null) Website = website;
        if (location is not null) Location = location;
        if (employeeCount is not null) EmployeeCount = employeeCount;
    }
}
