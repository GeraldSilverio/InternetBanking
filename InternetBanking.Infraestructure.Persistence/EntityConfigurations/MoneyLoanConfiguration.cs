using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBanking.Infraestructure.Persistence.EntityConfigurations
{
    public class MoneyLoanConfiguration : IEntityTypeConfiguration<MoneyLoan>
    {
        public void Configure(EntityTypeBuilder<MoneyLoan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.MoneyLoanCode).IsUnique();
            builder.Property(x => x.BalancePaid).HasColumnType("Decimal").HasPrecision(12, 2);
            builder.Property(x => x.BorrowedBalance).HasColumnType("Decimal").HasPrecision(12, 2);
            builder.Property(x => x.LastModifiedBy).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false);
        }
    }
}
