using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Infrastructure
{
    public static class DbSeeder
    {
        public static void Seed(BankDbContext context)
        {
            if (!context.Accounts.Any())
            {
                var acc1 = new SavingsAccount("SAV1001", "Sayeed", 5000m);
                var acc2 = new CheckingAccount("CHK2001", "Habib", 2000, 1000);

                context.Accounts.AddRange(acc1, acc2);
                context.SaveChanges();
            }
        }
    }
}
