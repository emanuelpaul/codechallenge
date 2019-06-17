using System.Collections.Generic;
using System.Threading.Tasks;
using CodeChallenge.API.Models;

namespace CodeChallenge.API.Services.Abstract
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync();

        Task AddAsync(Company company);

        Task<Company> GetByIdAsync(int companynId);

        Task<Company> GetByIsinAsync(string isin);

        Task UpdateAsync(int companyId, Company company);

        Task DeleteAsync(int companynId);

        Task<bool> Exists(int id);

        Task<bool> CanIsinByUsed(string isin, int? existingCompanyId);
    }
}
