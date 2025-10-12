using Alternance.Domain.Common;

namespace Alternance.Domain.Entities;

public class Student : BaseEntity
{
    public Guid UserId { get; private set; }
    public Guid StudentId { get; private set; }
    public string? Phone { get; private set; }
    public string? Bio { get; private set; }
    public string? ResumeUrl { get; private set; }
    public List<string> Skills { get; private set; } = new();
    public string? Location { get; private set; }
    public int? ExperienceYears { get; private set; }

    private Student() { } // For ORM

    public Student(Guid userId, Guid studentId)
    {
        UserId = userId;
        StudentId = studentId;
    }

    public void UpdateProfile(string? phone, string? bio, string? location, string? resumeUrl, List<string> skills, int? experienceYears)
    {
        Phone = phone;
        Bio = bio;
        Location = location;
        ResumeUrl = resumeUrl;
        Skills = skills;
        ExperienceYears = experienceYears;
    }

    public void AddSkill(string skill)
    {
        if (!Skills.Contains(skill))
        {
            Skills.Add(skill);
        }
    }

    public void RemoveSkill(string skill)
    {
        Skills.Remove(skill);
    }
}
