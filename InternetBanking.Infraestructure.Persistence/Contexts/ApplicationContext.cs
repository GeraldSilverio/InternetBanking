﻿using InternetBanking.Core.Domain.Commons;
using InternetBanking.Core.Domain.Entities;
using InternetBanking.Infraestructure.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace InternetBanking.Infraestructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new BeneficiaryConfiguration());
            modelBuilder.ApplyConfiguration(new CreditsCardConfiguration());
            modelBuilder.ApplyConfiguration(new MoneyLoanConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new SavingAccountConfiguration());
            modelBuilder.ApplyConfiguration(new EffectiveProgressConfiguration());
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


        #region DbSets
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<CreditsCard> CreditsCards { get; set; }
        public DbSet<MoneyLoan> MoneyLoans { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<EffectiveProgress> EffectiveProgress { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion


    }
}
