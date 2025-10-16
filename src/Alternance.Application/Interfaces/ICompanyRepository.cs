using Alternance.Domain.Entities;

namespace Alternance.Application.Interfaces;

public interface ICompanyRepository
{
    Task<Company?> GetCompanyByIdAsync(Guid id);
    Task<List<Company>> GetAllCompaniesAsync();
    Task<Company> AddCompanyAsync(Company company);
    Task UpdateCompanyAsync(Company company);
    Task DeleteCompanyAsync(Guid id);
}
