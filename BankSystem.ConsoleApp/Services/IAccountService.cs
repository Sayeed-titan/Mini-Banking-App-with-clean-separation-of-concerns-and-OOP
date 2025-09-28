using BankSystem.ConsoleApp.Core.Models;
using System.Collections.Generic;

namespace BankSystem.ConsoleApp.Services
{
    public interface IAccountService
    {
        Account CreateSavings(string accountNumber, string ownerName, decimal initialBalance, User owner);
        Account CreateChecking(string accountNumber, string ownerName, decimal initialBalance, decimal overdraftLimit, User owner);

        Account? GetByAccountNumber(string accountNumber);
        IEnumerable<Account> GetAllAccounts();

        void Deposit(string accountNumber, decimal amount);
        void Withdraw(string accountNumber, decimal amount);
    }
}
