using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BankSystem.ConsoleApp.Services
{
    public interface ITransactionService
    {
        void RecordTransaction(Transaction tx);
        IEnumerable<Transaction> GetTransactionsForAccount(string accountNumber);
        IEnumerable<Transaction> GetAllTransactions();
    }
}
