using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetCompanyByIdQuery(Guid Id) : IRequest<CompanyDto?>
{
    public Guid CompanyId { get; } = Id;
}
