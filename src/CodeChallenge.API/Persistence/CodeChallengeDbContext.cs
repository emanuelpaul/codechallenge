using CodeChallenge.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.API.Persistence
{
    public class CodeChallengeDbContext : DbContext
    {
        public CodeChallengeDbContext(DbContextOptions<CodeChallengeDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyDbMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Company> Companies { get; set; }
    }
}
