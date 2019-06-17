using CodeChallenge.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.API.Persistence
{
    public class CompanyDbMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.ForSqlServerHasIndex(x => x.Isin).IsUnique();
            builder.Property(x => x.Exchange).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Isin).IsRequired().HasMaxLength(12);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Ticker).IsRequired().HasMaxLength(5);
            builder.Property(x => x.Website).HasMaxLength(120);
        }
    }
}
