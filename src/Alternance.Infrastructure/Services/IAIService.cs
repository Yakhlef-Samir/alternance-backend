namespace Alternance.Infrastructure.Services;

public interface IAIService
{
    Task<string> GenerateTextAsync(string prompt);
    Task<List<string>> ExtractSkillsFromResumeAsync(string resumeText);
    Task<int> CalculateMatchScoreAsync(string jobDescription, string candidateProfile);
}
