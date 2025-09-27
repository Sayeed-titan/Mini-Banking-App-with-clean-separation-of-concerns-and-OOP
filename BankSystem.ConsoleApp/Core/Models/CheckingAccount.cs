using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; private set; }

        public CheckingAccount(string accountNumber, string ownerName, decimal initialBalance = 0, decimal overdraftLimit = 0) : base(accountNumber, ownerName, initialBalance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount, string? description = null)
        {
            if (amount <= 0) throw new ArgumentException("Withdraw amount must be positive",nameof(amount));

            if ( Balance + OverdraftLimit < amount)
            {
                throw new InvalidOperationException("Overdraft limit exceeded (CheckingAccount).");
            }

            Balance -= amount;
        }
    }
}
