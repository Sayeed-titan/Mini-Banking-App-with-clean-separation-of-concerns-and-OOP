using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, string ownerName, decimal initialBalance = 0) : base(accountNumber, ownerName, initialBalance)
        {
        }

        public override void Withdraw(decimal amount, string? description = null)
        {
            if (amount <= 0) throw new ArgumentException("Withdraw amount must be positive", nameof(amount));

            if (Balance < amount)
            {
                throw new InvalidOperationException("Insufficient funds for withdraw (SavingsAccount). ");
            }

            Balance -= amount;
        }
    }
}
