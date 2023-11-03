using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBanking.Infraestructure.Persistence.EntityConfigurations
{
    public class BeneficiaryConfiguration : IEntityTypeConfiguration<Beneficiary>
    {
        public void Configure(EntityTypeBuilder<Beneficiary> builder)
        {
            builder.ToTable("Benefiaciary");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.AccountCode).IsUnique();


            builder.Property(x => x.LastModifiedBy).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false);
        }
    }
}
