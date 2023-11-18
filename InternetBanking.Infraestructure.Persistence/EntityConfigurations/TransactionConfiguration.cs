using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBanking.Infraestructure.Persistence.EntityConfigurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount).HasColumnType("Decimal")
                .HasPrecision(16, 2);

            builder.Property(x => x.Date).HasColumnType("Date").HasConversion(
            v => v,
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc).Date);

            builder.Property(x => x.LastModifiedBy).IsRequired(false);
        }
    }
}
