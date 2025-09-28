using BankSystem.ConsoleApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.ConsoleApp.Core.Data
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")
                .HasValue<SavingsAccount>("Savings")
                .HasValue<CheckingAccount>("Checking");

            base.OnModelCreating(modelBuilder);
        }
    }
}
