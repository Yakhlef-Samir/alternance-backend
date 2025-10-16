using Alternance.Application.DTOs;
using Alternance.Application.Interfaces;
using Alternance.Application.Queries;
using MediatR;

namespace Alternance.Application.Handlers.Company;

public class GetAllCompaniesQueryHandler : IRequestHandler<GetAllCompaniesQuery, List<CompanyDto>>
{
    private readonly ICompanyRepository _companyRepository;

    public GetAllCompaniesQueryHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<List<CompanyDto>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var companies = await _companyRepository.GetAllCompaniesAsync();

        return companies.Select(company => new CompanyDto(
            company.Id,
            company.CompanyId,
            company.UserId,
            company.CompanyName,
            company.Description,
            company.Industry,
            company.Website,
            company.Location,
            company.EmployeeCount
        )).ToList();
    }
}
