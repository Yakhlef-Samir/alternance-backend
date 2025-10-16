namespace Alternance.Application.DTOs;

public record CompanyDto(
    Guid Id,
    Guid CompanyId,
    Guid UserId,
    string CompanyName,
    string? Description,
    string? Industry,
    string? Website,
    string? Location,
    int? EmployeeCount
);
