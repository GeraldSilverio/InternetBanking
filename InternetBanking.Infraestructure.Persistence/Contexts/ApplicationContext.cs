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
        }

        #region DbSets


        #endregion

        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<CreditsCard> CreditsCards { get; set; }
    }
}
