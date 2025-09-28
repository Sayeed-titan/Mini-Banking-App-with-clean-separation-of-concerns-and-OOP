namespace BankSystem.ConsoleApp.Core.Models
{
    public class SavingsAccount : Account
    {
        public SavingsAccount() : base() { } // For EF

        public SavingsAccount(string accountNumber, string ownerName, decimal initialBalance)
            : base(accountNumber, ownerName, initialBalance) { }

        // Could add interest logic here later
    }
}
