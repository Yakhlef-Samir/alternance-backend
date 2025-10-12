using Alternance.Domain.Entities;
using Alternance.Domain.Enum;

namespace Alternance.Domain.Business;

public class MatchingAlgorithm
{
    public List<Job> FindMatchingJobs(Student student, List<Job> availableJobs)
    {
        var matchedJobs = new List<(Job job, int score)>();

        foreach (var job in availableJobs.Where(j => j.Status == Status.Active))
        {
            int score = CalculateJobMatchScore(student, job);
            if (score > 0)
            {
                matchedJobs.Add((job, score));
            }
        }

        return matchedJobs
            .OrderByDescending(x => x.score)
            .Select(x => x.job)
            .ToList();
    }

    public List<Student> FindMatchingCandidates(Job job, List<Student> candidates)
    {
        var matchedCandidates = new List<(Student student, int score)>();

        foreach (var student in candidates)
        {
            int score = CalculateCandidateMatchScore(student, job);
            if (score > 0)
            {
                matchedCandidates.Add((student, score));
            }
        }

        return matchedCandidates
            .OrderByDescending(x => x.score)
            .Select(x => x.student)
            .ToList();
    }

    private int CalculateJobMatchScore(Student student, Job job)
    {
        int score = 0;

        // Skills matching
        foreach (var skill in student.Skills)
        {
            if (job.RequiredSkills.Contains(skill, StringComparer.OrdinalIgnoreCase))
            {
                score += 20;
            }
        }

        // Location matching
        if (!string.IsNullOrEmpty(student.Location) && 
            student.Location.Equals(job.Location, StringComparison.OrdinalIgnoreCase))
        {
            score += 10;
        }

        return score;
    }

    private int CalculateCandidateMatchScore(Student student, Job job)
    {
        return CalculateJobMatchScore(student, job);
    }
}
