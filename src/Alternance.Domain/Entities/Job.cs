using Alternance.Domain.Common;
using Alternance.Domain.Enum;
namespace Alternance.Domain.Entities;

public class Job : BaseEntity
{
    public Guid JobId { get; private set; }
    public Guid CompanyId { get; private set; }

    public string Title { get; private set; } = string.Empty;
    
    public string Description { get; private set; } = string.Empty;

    public string Location { get; private set; } = string.Empty;


    public ContractType ContractType { get; private set; } // "Alternance", "Stage", "CDI"

    public decimal Salary { get; private set; }

    public List<string> RequiredSkills { get; private set; } = new();

    public Status Status { get; private set; } = Status.Active; // "Active", "Closed", "Draft"

    public Job(Guid jobId, Guid companyId, string title, string description, string location, ContractType contractType, decimal salary)
    {
        JobId = jobId;
        CompanyId = companyId;
        Title = title;
        Description = description;
        Location = location;
        ContractType = contractType;
        Salary = salary;
    }

    public void Publish()
    {
        Status = Status.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Close()
    {
        Status = Status.Closed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRequiredSkill(string skill)
    {
        if (!RequiredSkills.Contains(skill))
        {
            RequiredSkills.Add(skill);
        }
    }
}
