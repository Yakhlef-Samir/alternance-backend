using Alternance.Domain.Entities;
using Alternance.Domain.Enum;

namespace Alternance.Domain.Business;

public class ApplicationWorkflow
{
    public static bool CanApply(Student student, Job job)
    {
        // Business rules for application
        if (string.IsNullOrEmpty(student.ResumeUrl))
            return false;

        if (job.Status != Status.Active)
            return false;

        return true;
    }

    public static bool CanReview(Application application)
    {
        return application.Status == ApplicationStatus.Pending;
    }

    public static void ProcessApplication(Application application)
    {
        // Complex business logic for processing applications
        application.Review();
    }

    public static int CalculateMatchScore(Student student, Job job)
    {
        // Calculate matching score based on skills and requirements
        int score = 0;
        
        foreach (var skill in student.Skills)
        {
            if (job.RequiredSkills.Contains(skill))
            {
                score += 10;
            }
        }

        return score;
    }
}
