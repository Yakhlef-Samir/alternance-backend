using MediatR;

namespace Alternance.Application.Commands;

public record CreateJobCommand(
    Guid CompanyId,
    string Title,
    string Description,
    string Location,
    string ContractType,
    decimal Salary
) : IRequest<Guid>;
