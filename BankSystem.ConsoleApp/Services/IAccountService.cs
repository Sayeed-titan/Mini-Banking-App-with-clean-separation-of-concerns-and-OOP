using BankSystem.ConsoleApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.ConsoleApp.Services
{
    public interface IAccountService
    {
        Account CreateSavings(string accountNumber, string ownerName, decimal initialBalance = 0m);
        Account CreateChecking(string accountNumber, string ownerName, decimal initialBalance = 0m, decimal overdraftLimit = 500m);
        Account? GetByAccountNumber (string accountNumber);
        IEnumerable<Account> GetAccounts();

    }
}
