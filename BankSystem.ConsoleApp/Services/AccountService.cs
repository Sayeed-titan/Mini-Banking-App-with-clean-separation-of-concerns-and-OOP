using BankSystem.ConsoleApp.Core.Data;
using BankSystem.ConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.ConsoleApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankDbContext _context;

        public AccountService(BankDbContext context)
        {
            _context = context;
        }

        public Account CreateSavings(string accountNumber, string ownerName, decimal initialBalance, User owner)
        {
            var acc = new SavingsAccount(accountNumber, ownerName, initialBalance, owner);
            _context.Accounts.Add(acc);
            _context.SaveChanges();
            return acc;
        }

        public Account CreateChecking(string accountNumber, string ownerName, decimal initialBalance, decimal overdraftLimit, User owner)
        {
            var acc = new CheckingAccount(accountNumber, ownerName, initialBalance, overdraftLimit, owner);
            _context.Accounts.Add(acc);
            _context.SaveChanges();
            return acc;
        }

        public Account? GetByAccountNumber(string accountNumber)
        {
            return _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public void Deposit(string accountNumber, decimal amount)
        {
            var acc = GetByAccountNumber(accountNumber) ?? throw new InvalidOperationException("Account not found.");
            acc.Deposit(amount);
            _context.SaveChanges();
        }

        public void Withdraw(string accountNumber, decimal amount)
        {
            var acc = GetByAccountNumber(accountNumber) ?? throw new InvalidOperationException("Account not found.");
            acc.Withdraw(amount);
            _context.SaveChanges();
        }
    }
}
