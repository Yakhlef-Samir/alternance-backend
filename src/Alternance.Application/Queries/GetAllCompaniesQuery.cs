using Alternance.Application.DTOs;
using MediatR;

namespace Alternance.Application.Queries;

public record GetAllCompaniesQuery : IRequest<List<CompanyDto>>;
