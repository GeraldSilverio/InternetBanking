
using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBanking.Infraestructure.Persistence.EntityConfigurations
{
    public class EffectiveProgressConfiguration : IEntityTypeConfiguration<EffectiveProgress>
    {
        public void Configure(EntityTypeBuilder<EffectiveProgress> builder)
        {
            builder.ToTable("EffectiveProgress");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasColumnType("Decimal").HasPrecision(12, 2);
            builder.Property(x => x.LastModifiedBy).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false);
        }
    }
}
