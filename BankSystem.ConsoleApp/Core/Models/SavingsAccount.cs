namespace BankSystem.ConsoleApp.Core.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount() { } // EF Core

        public SavingsAccount(string accountNumber, string ownerName, decimal initialBalance, User user)
            : base(accountNumber, ownerName, initialBalance, user) { }
    }
}
