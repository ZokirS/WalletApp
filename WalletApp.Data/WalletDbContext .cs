using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace WalletApp.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RechargeOperation> RechargeOperations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(a => a.RechargeOperations)
                .WithOne(ro => ro.Account)
                .HasForeignKey(ro => ro.AccountId);
        }

    }
}
