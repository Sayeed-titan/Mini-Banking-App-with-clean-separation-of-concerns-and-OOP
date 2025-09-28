using System;

namespace BankSystem.ConsoleApp.Core.Models
{
    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; set; }

        public CheckingAccount() : base() { } // For EF

        public CheckingAccount(string accountNumber, string ownerName, decimal initialBalance, decimal overdraftLimit)
            : base(accountNumber, ownerName, initialBalance)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount, string? description = null)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Withdrawal must be positive.");
            if (Balance + OverdraftLimit < amount)
                throw new InvalidOperationException("Insufficient funds (including overdraft).");

            Balance -= amount;
        }
    }
}
