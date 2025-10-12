using Alternance.Domain.Common;
using Alternance.Domain.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace Alternance.Domain.Entities;

public class Application : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Guid JobId { get; private set; }
    public string CoverLetter { get; private set; } = string.Empty;
    public string ResumeUrl { get; private set; } = string.Empty;
    public ApplicationStatus Status { get; private set; } = ApplicationStatus.Pending; // "Pending", "Reviewed", "Accepted", "Rejected"
    public DateTime AppliedAt { get; private set; }
    public DateTime? ReviewedAt { get; private set; }

    private Application() { } // For ORM

    public Application(Guid studentId, Guid jobId, string coverLetter, string resumeUrl)
    {
        StudentId = studentId;
        JobId = jobId;
        CoverLetter = coverLetter;
        ResumeUrl = resumeUrl;
        AppliedAt = DateTime.UtcNow;
        Status = ApplicationStatus.Pending;
    }

    public void Review()
    {
        Status = ApplicationStatus.Reviewed;
        ReviewedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        Status = ApplicationStatus.Accepted;
        ReviewedAt = DateTime.UtcNow;
    }

    public void Reject()
    {
        Status = ApplicationStatus.Rejected;
        ReviewedAt = DateTime.UtcNow;
    }
}
