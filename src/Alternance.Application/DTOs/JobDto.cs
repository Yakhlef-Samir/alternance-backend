namespace Alternance.Application.DTOs;

public record JobDto(
    Guid Id,
    Guid CompanyId,
    string Title,
    string Description,
    string Location,
    string ContractType,
    decimal Salary,
    DateTime CreatedAt
);
