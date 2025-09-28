namespace BankSystem.ConsoleApp.Core.Models
{
    public class CheckingAccount : Account
    {
        public decimal OverdraftLimit { get; private set; }

        public CheckingAccount() { }

        public CheckingAccount(string accountNumber, string ownerName, decimal initialBalance, decimal overdraftLimit, User user)
            : base(accountNumber, ownerName, initialBalance, user)
        {
            OverdraftLimit = overdraftLimit;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new InvalidOperationException("Withdrawal must be positive.");
            if (Balance + OverdraftLimit < amount) throw new InvalidOperationException("Insufficient funds including overdraft.");
            AdjustBalance( amount);
        }
    }
}
