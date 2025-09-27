using BankSystem.ConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly Dictionary<string, Account> _account = new();

        public Account CreateChecking(string accountNumber, string ownerName, decimal initialBalance = 0, decimal overdraftLimit = 500)
        {
            ThrowIfExists(accountNumber);

            var acc = new CheckingAccount (accountNumber, ownerName, initialBalance, overdraftLimit);
            _account[accountNumber] = acc;

            return acc;
        }

        private void ThrowIfExists(string accountNumber)
        {
            if (_account.ContainsKey(accountNumber))
                throw new InvalidOperationException($"Account with number {accountNumber} already exists.");
        }

        public Account CreateSavings(string accountNumber, string ownerName, decimal initialBalance = 0)
        {
            ThrowIfExists (accountNumber);

            var acc = new SavingsAccount (accountNumber, ownerName, initialBalance);
            _account[accountNumber] = acc;

            return acc;
        }

        public IEnumerable<Account> GetAccounts() => _account.Values.ToList();

        public Account? GetByAccountNumber(string accountNumber)
        {
            _account.TryGetValue(accountNumber, out var acc);

            return acc;
        }
    }
}
