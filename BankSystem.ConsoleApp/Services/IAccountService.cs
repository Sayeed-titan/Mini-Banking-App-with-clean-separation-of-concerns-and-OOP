using BankSystem.ConsoleApp.Core.Models;
using System.Collections.Generic;

namespace BankSystem.ConsoleApp.Services
{
    public interface IAccountService
    {
        Account CreateSavings(string accountNumber, string ownerName, decimal initialBalance);
        Account CreateChecking(string accountNumber, string ownerName, decimal initialBalance, decimal overdraftLimit);

        Account? GetByAccountNumber(string accountNumber);
        IEnumerable<Account> GetAllAccounts();

        void Deposit(string accountNumber, decimal amount);
        void Withdraw(string accountNumber, decimal amount, string? description = null);
    }
}
