﻿using InternetBanking.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetBanking.Infraestructure.Persistence.EntityConfigurations
{
    public class SavingAccountConfiguration : IEntityTypeConfiguration<SavingAccount>
    {
        public void Configure(EntityTypeBuilder<SavingAccount> builder)
        {
            builder.ToTable("SavingAccount");
            builder.HasKey(x=> x.Id);
            builder.HasIndex(x => x.AccountCode).IsUnique();
            builder.Property(x => x.Balance).HasColumnType("Decimal").HasPrecision(12, 2);
            builder.Property(x => x.LastModifiedBy).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false);
        }
    }
}
