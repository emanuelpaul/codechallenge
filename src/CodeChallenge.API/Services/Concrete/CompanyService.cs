using System.Collections.Generic;
using System.Threading.Tasks;
using CodeChallenge.API.Models;
using CodeChallenge.API.Persistence;
using CodeChallenge.API.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.API.Services.Concrete
{
    public class CompanyService : ICompanyService
    {
        private readonly CodeChallengeDbContext _dbContext;

        public CompanyService(CodeChallengeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Company company)
        {
            _dbContext.Companies.Add(company);

            return _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(int companynId)
        {
            _dbContext.Companies.Remove(new Company { CompanyId = companynId });

            return _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync() =>
            await _dbContext.Companies.ToListAsync();

        public Task<Company> GetByIdAsync(int companynId) =>
            _dbContext.Companies.FindAsync(companynId);

        public Task<Company> GetByIsinAsync(string isin) =>
            _dbContext.Companies.FirstOrDefaultAsync(x => x.Isin == isin);

        public Task UpdateAsync(int companyId, Company company)
        {
            company.CompanyId = companyId;
            _dbContext.Companies.Update(company);

            return _dbContext.SaveChangesAsync();
        }

        public Task<bool> Exists(int id) => _dbContext.Companies.AnyAsync(x => x.CompanyId == id);

        public Task<bool> Exists(string isin) =>
            _dbContext.Companies.AnyAsync(x => x.Isin == isin);
    }
}
