using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBanking.Infraestructure.Persistence.EntityConfigurations
{
    public class CreditsCardConfiguration : IEntityTypeConfiguration<CreditsCard>
    {
        public void Configure(EntityTypeBuilder<CreditsCard> builder)
        {
            builder.ToTable("CreditsCard");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.CardNumber).IsUnique();
            builder.Property(x => x.CreditLimited).HasColumnType("Decimal").HasPrecision(12, 2);
            builder.Property(x => x.Available).HasColumnType("Decimal").HasPrecision(12, 2);
            builder.Property(x => x.Spent).HasColumnType("Decimal").HasPrecision(12, 2);
            builder.Property(x => x.LastModifiedBy).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false);
        }
    }
}
