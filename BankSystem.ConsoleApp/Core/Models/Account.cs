using System;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.ConsoleApp.Core.Models
{
    public abstract class Account
    {
        [Key]
        public string AccountNumber { get; set; } = null!;
        public string OwnerName { get; set; } = null!;

        // Allow setting balance inside services & EF
        public decimal Balance { get; set; }

        protected Account() { }  // Needed for EF Core

        protected Account(string accountNumber, string ownerName, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = initialBalance;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Deposit must be positive.");
            Balance += amount;
        }

        public virtual void Withdraw(decimal amount, string? description = null)
        {
            if (amount <= 0)
                throw new InvalidOperationException("Withdrawal must be positive.");
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }

        public override string ToString()
        {
            return $"{AccountNumber} | {OwnerName} | Balance: {Balance:C}";
        }
    }
}
