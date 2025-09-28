using BankSystem.ConsoleApp.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BankSystem.ConsoleApp.Infrastructure
{
    public class BankDbContextFactory : IDesignTimeDbContextFactory<BankDbContext>
    {
        public BankDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=BankConsoleDB;Trusted_Connection=True;TrustServerCertificate=True;"
            );

            return new BankDbContext(optionsBuilder.Options);
        }
    }
}
