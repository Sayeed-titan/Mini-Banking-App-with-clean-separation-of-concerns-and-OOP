using System.ComponentModel.DataAnnotations;

namespace BankSystem.ConsoleApp.Core.Models
{
    public abstract class Account
    {
        [Key]
        public string AccountNumber { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public decimal Balance { get; private set; }

        // Navigation to User
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        protected Account() { }

        protected Account(string accountNumber, string ownerName, decimal initialBalance, User user)
        {
            AccountNumber = accountNumber;
            OwnerName = ownerName;
            Balance = initialBalance;
            User = user;
            UserId = user.Id;
        }

        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0) throw new InvalidOperationException("Deposit must be positive.");
            Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new InvalidOperationException("Withdrawal must be positive.");
            if (Balance < amount) throw new InvalidOperationException("Insufficient funds.");
            Balance -= amount;
        }

        public override string ToString()
        {
            return $"{AccountNumber} | {OwnerName} | Balance: {Balance:C}";
        }

        protected void AdjustBalance(decimal amount)
        {
            Balance += amount;
        }

    }
}
