using BankSystem.ConsoleApp.Core.Models;
using System.Collections.Generic;

namespace BankSystem.ConsoleApp.Services
{
    public interface ITransactionService
    {
        void RecordTransaction(Transaction tx);
        IEnumerable<Transaction> GetTransactionsForAccount(string accountNumber);
        IEnumerable<Transaction> GetAllTransactions();
    }
}
